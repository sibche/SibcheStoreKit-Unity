#import <SibcheStoreKit/SibcheStoreKit.h>
#import "UnityAppController.h"
#import <Foundation/Foundation.h>

// Converts NSString to C style string by way of copy (Mono will free it)
#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

// Converts C style string to NSString as long as it isnt empty
#define GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil

void UnitySendMessage(const char *className, const char *methodName, const char *param);

void SafeUnitySendMessage(const char *methodName, const char *param) {
    if (methodName == NULL) {
        methodName = "";
    }
    if (param == NULL) {
        param = "";
    }
    UnitySendMessage("SibcheManager", methodName, param);
}

@interface SibcheUnityManager : NSObject

- (void)onOpenUrl:(NSURL*)url;

@end

@implementation SibcheUnityManager

+ (void)load{
    [self sharedManager];
}

+ (id)sharedManager{
    static SibcheUnityManager *sharedManager = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedManager = [[self alloc] init];
    });
    return sharedManager;
}

- (id)init {
    if ( self = [super init] ) {
        __weak SibcheUnityManager *weakSelf = self;
        
        [[NSNotificationCenter defaultCenter] addObserverForName:@"kUnityOnOpenURL" object:nil queue:nil usingBlock:^(NSNotification * _Nonnull note) {
            NSURL* url = [note.userInfo valueForKeyPath:@"url"];
            [weakSelf onOpenUrl:url];
        }];
    }
    return self;
}

- (void)dealloc {
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}

- (void)onOpenUrl:(NSURL*)url{
    [SibcheStoreKit openUrl:url options:nil];
}

@end

NSString* changeDictionaryToJson(NSDictionary* dictionary){
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dictionary
                                                       options:0
                                                         error:&error
                        ];
    
    if (! jsonData) {
        NSLog(@"%s: error: %@", __func__, error.localizedDescription);
        return @"";
    } else {
        NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        return jsonString;
    }
}

extern "C" {
    void _SibcheStoreKitInit(const char *appKey, const char *appScheme){
        [SibcheStoreKit initWithApiKey:GetStringParam(appKey) withScheme:GetStringParam(appScheme) withStore:@"SDK-UNITY"];
    }
    
    void _SibcheStoreKitLogin(){
        [SibcheStoreKit loginUser:^(BOOL isLoginSuccessful, SibcheError* error, NSString *userName, NSString *userId) {
            NSDictionary* dictionary = @{
                                         @"isLoginSuccessful": isLoginSuccessful ? @1 : @0,
                                         @"error": error ? [error toJson] : @"",
                                         @"userName": userName ? userName : @"",
                                         @"userId": userId ? userId : @"",
                                         };
            NSString* str = changeDictionaryToJson(dictionary);
            SafeUnitySendMessage(MakeStringCopy(@"NotifyLogin"),MakeStringCopy(str));
        }];
    }
    
    void _SibcheStoreKitLogout(){
        [SibcheStoreKit logoutUser:^{
            SafeUnitySendMessage(MakeStringCopy(@"NotifyLogout"),MakeStringCopy(@""));
        }];
    }
    
    void _SibcheStoreKitFetchInAppPurchasePackages(){
        [SibcheStoreKit fetchInAppPurchasePackages:^(BOOL isSuccessful, SibcheError *error, NSArray *packagesArray) {
            NSMutableDictionary* dictionary = [NSMutableDictionary dictionaryWithDictionary:@{
                                         @"isSuccessful": isSuccessful ? @1 : @0,
                                         @"error": error ? [error toJson] : @"",
                                         }];
            if (packagesArray && packagesArray.count > 0) {
                NSMutableArray* array = [[NSMutableArray alloc] init];
                for (int i = 0; i < packagesArray.count; i++) {
                    SibchePackage* package = packagesArray[i];
                    [array addObject:[package toJson]];
                }
                
                [dictionary setObject:array forKey:@"packagesArray"];
            }
            NSString* str = changeDictionaryToJson(dictionary);
            SafeUnitySendMessage(MakeStringCopy(@"NotifyFetchPackages"),MakeStringCopy(str));
        }];
    }
    
    void _SibcheStoreKitFetchInAppPurchasePackage(const char *packageId){
        [SibcheStoreKit fetchInAppPurchasePackage:GetStringParam(packageId) withPackagesCallback:^(BOOL isSuccessful, SibcheError *error, SibchePackage *package) {
            NSDictionary* dictionary = @{
                                         @"isSuccessful": isSuccessful ? @1 : @0,
                                         @"error": error ? [error toJson] : @"",
                                         @"package": package ? [package toJson] : @"",
                                         };
            NSString* str = changeDictionaryToJson(dictionary);
            SafeUnitySendMessage(MakeStringCopy(@"NotifyFetchPackage"),MakeStringCopy(str));
        }];
    }
    
    void _SibcheStoreKitFetchActiveInAppPurchasePackages(){
        [SibcheStoreKit fetchActiveInAppPurchasePackages:^(BOOL isSuccessful, SibcheError *error, NSArray *purchasePackagesArray) {
            NSMutableDictionary* dictionary = [NSMutableDictionary dictionaryWithDictionary:@{
                                                                                              @"isSuccessful": isSuccessful ? @1 : @0,
                                                                                              @"error": error ? [error toJson] : @"",
                                                                                              }];
            if (purchasePackagesArray && purchasePackagesArray.count > 0) {
                NSMutableArray* array = [[NSMutableArray alloc] init];
                for (int i = 0; i < purchasePackagesArray.count; i++) {
                    SibchePackage* package = purchasePackagesArray[i];
                    [array addObject:[package toJson]];
                }
                
                [dictionary setObject:array forKey:@"purchasePackagesArray"];
            }
            NSString* str = changeDictionaryToJson(dictionary);
            SafeUnitySendMessage(MakeStringCopy(@"NotifyFetchActivePackages"),MakeStringCopy(str));
        }];
    }
    
    void _SibcheStoreKitPurchasePackage(const char *packageId){
        [SibcheStoreKit purchasePackage:GetStringParam(packageId) withCallback:^(BOOL isSuccessful, SibcheError *error, SibchePurchasePackage *purchasePackage) {
            NSDictionary* dictionary = @{
                                          @"isSuccessful": isSuccessful ? @1 : @0,
                                          @"error": error ? [error toJson] : @"",
                                          @"purchasePackage": purchasePackage ? [purchasePackage toJson] : @"",
                                          };
            NSString* str = changeDictionaryToJson(dictionary);
            SafeUnitySendMessage(MakeStringCopy(@"NotifyPurchase"),MakeStringCopy(str));
        }];
    }
    
    void _SibcheStoreKitConsumePackage(const char *purchasePackageId){
        [SibcheStoreKit consumePurchasePackage:GetStringParam(purchasePackageId) withCallback:^(BOOL isSuccessful, SibcheError *error) {
            NSDictionary* dictionary = @{
                                         @"isSuccessful": isSuccessful ? @1 : @0,
                                         @"error": error ? [error toJson] : @"",
                                         };
            NSString* str = changeDictionaryToJson(dictionary);
            SafeUnitySendMessage(MakeStringCopy(@"NotifyConsume"),MakeStringCopy(str));
        }];
    }
    
    void _SibcheStoreKitGetCurrentUserData(){
        [SibcheStoreKit getCurrentUserData:^(BOOL isSuccessful, SibcheError *error, LoginStatusType loginStatus, NSString *userCellphoneNumber, NSString *userId) {
            NSDictionary* dictionary = @{
                                         @"isSuccessful": isSuccessful ? @1 : @0,
                                         @"error": error ? [error toJson] : @"",
                                         @"loginStatus": [NSNumber numberWithInt:loginStatus],
                                         @"userCellphoneNumber": userCellphoneNumber,
                                         @"userId": userId,
                                         };
            NSString* str = changeDictionaryToJson(dictionary);
            SafeUnitySendMessage(MakeStringCopy(@"NotifyGetCurrentUserData"),MakeStringCopy(str));
        }];
    }
}
