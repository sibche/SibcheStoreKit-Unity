# &#x202b;نصب کیت توسعه‌دهندگان برای بازی‌های یونیتی

## نیازمندیهای نصب کیت توسعه دهندگان

1. بارگزاری برنامه در [پنل توسعه‌دهندگان سیبچه](https://sibche.com/developer)
2. دریافت کلید برنامه از پنل توسعه‌دهندگان
3. آخرین نسخه‌ی Xcode
4. داشتن unity v2019.1 به بالا

## نصب کیت توسعه‌دهندگان

کیت توسعه‌دهندگان سیبچه مختص یونیتی را می‌توانید از [اینجا](https://github.com/sibche/SibcheStoreKit-Unity/releases/download/1.0.0/SibcheStoreKit.unitypackage) دانلود کرده و به پروژه خود اضافه کنید. 

## تنظیمات اولیه

تنظیمات مورد نیاز پروژه به صورت خودکار داخل پروژه در زیربخش 
`Editor`
 اضافه شده است که فایلی با نام
`SibcheBuildPostProcessor.cs`
می‌باشد.


این فایل شامل دو بخش تنظیم می‌باشد که یکی برای اضافه کردن scheme یا همان url اختصاصی برای بازی شما می‌باشد و بخش دیگر، برای اضافه کردن فایل پروژه به صورت داینامیک به پروژه می‌باشد. در مورد هر دو بخش به تفضیل توضیح خواهیم داد. 

### افزودن Scheme مختص برنامه شما

اگر برنامه شما دارای scheme یا url اختصاصی هست، بایستی این تنظیم را از بخش 
`SibcheBuildPostProcessor.cs`
غیرفعال (کامنت) کنید.

همانند کد زیر می‌توانید url اختصاصی خود را بر روی پروژه تنظیم نمایید.

```C#
AddCustomUrlScheme(path, "testapp", "test");
```

به جای test، نام دلخواهی را تنظیم کرده و به جای testapp بایستی scheme مورد نظرتان را وارد نمایید. به عنوان مثال scheme تنظیم شده **تلگرام** `tg` و scheme تنظیم شده برای برنامه **اینستاگرام** `instagram` میباشد. لیست کامل scheme برنامه‌های معروف را میتوانید از
[اینجا](https://ios.gadgethacks.com/news/always-updated-list-ios-app-url-scheme-names-0184033/)
مشاهده نمایید.

<aside class="warning">
توصیه اکید می‌شود از scheme استفاده کنید که مختص شما باشد و ترجیحا طولانی باشد. توجه فرمایید که اگر با برنامه‌های دیگر در تداخل باشد، در فرآیند پرداخت دچار مشکل خواهید شد.
</aside>

### افزودن فایل .framework به عنوان کتابخانه دینامیک (embedded framework)

این بخش نیز کلیه تنظیمات مربوط به دینامیک کردن پروژه را انجام داده و کافیست اجازه دهید به همان شکلی که هست باقی بماند:

```C#
AddSibcheFrameworkAsEmbed(path);
```

### افزودن init اولیه کتابخانه سیبچه

در جایی از ورودی کلاس اصلی (تابع Start) بایستی تابع init کتابخانه سیبچه را فراخوانی نمایید تا برای استفاده‌های آتی آماده شده باشد:

```C#
using SibcheStoreKit;

public class GameController : MonoBehaviour
{
    // ...

    private void Start()
    {
        //Other initiation tasks

        // Sibche StoreKit initiation
        Sibche.Initialize(YOUR_API_KEY, YOUR_SCHEME);
    }

    // ...
}
```

 به جای `YOUR_API_KEY` بایستی api key دریافت شده از پنل دولوپری سیبچه را وارد نمایید. همچنین به جای `YOUR_SCHEME‍` بایستی scheme تعریف شده از مراحل قبلی را وارد نمایید.

##کلاس‌های مورد استفاده در کیت توسعه‌دهندگان

برای خرید، فعال‌سازی و استفاده از بسته‌های سیبچه، کلاسهای زیر ایجاد شده است که در ادامه در مورد هر کدام توضیحاتی ارائه خواهیم کرد.

- `SibchePackage`:
این کلاس، نمایانگر بسته قابل خریدی هست که داخل پنل توسعه‌دهندگان اقدام به تعریفشان کرده‌اید. 

- `SibchePurchasePackage`:
این کلاس، نمایانگر خرید متناظری از کاربر هست که به یک بسته SibchePackage اختصاص یافته است. این کلاس، شامل اطلاعات خرید، زمان خرید، و تاریخ انقضا و ... خواهد بود.

- `SibcheError`:
این کلاس، در مواقع رخداد خطا به شما ارجاع داده خواهد شد که شامل اطلاعات بیشتر از خطاهای رخ داده است. اطلاعاتی از قبیل خطا و Http status code و ...
 
 
###SibchePackage
سه نوع بسته قابل خرید داریم که عبارتند از:

- بسته‌های قابل خرید مصرفی یا `SibcheConsumablePackage`:
 بسته‌هایی که قابل مصرف هستند که خریداری شده و داخل بازی یا برنامه مصرف می‌شود. مانند بسته‌ی ۵۰۰ سکه طلا یا شارژ قابل مصرف داخل برنامه.

این بسته ها تا زمانی که مصرف نشده‌اند، فعال و معتبر می‌باشند و پس از مصرف، قابل خرید مجدد هستند. 

- بسته‌های قابل خرید غیر مصرفی یا `SibcheNonConsumablePackage`:
بسته‌هایی که قابل مصرف نیستند و فقط یکبار خریداری می‌شوند. مانند بسته‌ی باز شدن قابلیت آپلود آواتار یا تغییر نام.

این بسته ها، فقط یکبار قابل خرید هستند و پس از خرید، همواره در لیست بسته‌های فعال کاربر خواهند بود.


- بسته‌های اشتراک یا `SibcheSubscriptionPackage`:
بسته‌هایی هستند که به مدت محدود قابل استفاده بوده و پس از اتمام زمان آنها، کاربر مجاز به استفاده از آن قابلیت نیست. به عنوان مثال، قابلیت استفاده از امکانات ویژه به مدت یک سال

همه این سه کلاس از کلاس والد `SibchePackage` گرفته شده‌اند و به صورت عمومی شامل خصوصیات زیر هستند.

```C#
public string packageId;
public string type;
public string code;
public string name;
public string packageDescription;
public int price;
public int totalPrice;
public int discount;
```

علاوه بر این توابع، کلاس `SibcheSubscriptionPackage` شامل خصوصیات اضافی‌تر زیر نیز هست:

```C#
public int duration;
public string group;
```

###SibchePurchasePackage

این کلاس، شامل تناظر خرید کاربر به بسته‌های شما می‌باشد. این کلاس شامل خصوصیات زیر است:

```C#
public string purchasePackageId;
public string type;
public string code;
public DateTime expireAt;
public DateTime createdAt;
public SibchePackage package;
```

###SibcheError

این کلاس در مواقع بروز خطا به شما داده خواهد شد و شامل خصوصیات زیر می‌باشد:

```C#
public string message;
public int errorCode;
public int statusCode;
```

- errorCode: همان شماره خطایی هست که مطابق جدول زیر ایجاد شده است.
- message: پیغام خطایی هست که از طرف سرور دریافت شده است.
- statusCode:  همان شماره خطای http هست که سرور در جواب درخواست ما داده است.


| شماره errorCode | دلیل خطای مربوطه                         |
| --------------- | ---------------------------------------- |
| 1000            | خطای نامشخص                              |
| 1001            | این بسته قبلا خریداری شده است            |
| 1002            | کاربر از ادامه عملیات منصرف شد                 |
| 1003            | در فرایند ورود (لاگین) دچار مشکل شده‌ایم |
| 1004            |  برنامه به درستی initiate نشده است |


## گرفتن لیست بسته‌های قابل خرید

پس از تنظیم برنامه، میتوانید بسته‌های قابل خرید را مشاهده نمایید. کافیست همانند کد زیر، اقدام به فراخوانی تابع مورد نظر نمایید:

```C#
Sibche.FetchPackages((bool isSuccessful, SibcheError error, List<SibchePackage> packages) =>
{
    if (isSuccessful)
    {
        // Success block
    } else
    {
        // failure block
        Debug.Log(error.message);
    }
});
```

در پاسخ، در صورت موفقیت، کیت توسعه‌دهندگان بسته‌های قابل خرید را به عنوان پارامتر پاسخ به شما تحویل میدهد. این پارامتر آرایه‌ای از بسته‌های قابل خرید می‌باشد. این بسته‌ها از نوع `SibchePackage` هستند. 

در صورت ناموفق بودن درخواست، پارمتری با نام error از نوع `‍‍SibcheError` ارسال می‌شود، وگرنه به صورت null داده خواهد شد.

## گرفتن اطلاعات بسته مشخص

با در اختیار داشتن آیدی یا کد باندل بسته مورد نظر می‌توانید اطلاعات آن بسته را در اختیار بگیرید. نحوه استفاده از این API به شکل زیر است:

```C#
// Both package bundle code & package id is acceptable
Sibche.FetchPackage(packageId, (bool isSuccessful1, SibcheError error, SibchePackage package) =>
{
    if (isSuccessful)
    {
        // Success block
    } else
    {
        // failure block
        Debug.Log(error.message);
    }
});
```

پارامتر اول داده شده، همان callback ارسال شده ما است که پس از مشخص شدن وضعیت درخواست، فراخوانی خواهد شد. در صورت موفقیت، بسته‌ی مورد نظر در قالب آبجکت `SibchePackage` (بسته به نوع بسته) به شما ارسال خواهد شد.

## خرید بسته مشخص

پس از گرفتن لیست بسته‌ها، میتوانید درخواست خرید این بسته‌ها را از طریق کد روبرو به کیت توسعه‌دهندگان بدهید. در ادامه، کیت توسعه‌دهندگان، در صورت نیاز کاربر را لاگین کرده و فرایند پرداخت را پیگیری خواهد کرد. سپس موفق یا ناموفق بودن خرید را به همراه `SibchePurchasePackage` به اطلاع شما خواهد رساند.

```C#
Sibche.Purchase(packageCode, (bool isSuccessful, SibcheError error, SibchePurchasePackage purchasedPackage) =>
{
    if (isSuccessful)
    {
        // Success block
    } else
    {
        // failure block
        Debug.Log(error.message);
    }
});
```

## گرفتن لیست بسته های خریداری شده

با استفاده از این دستور، میتوانید لیست بسته‌های فعال (خریداری شده) کاربر را بدست آورید. کافیست همانند کد زیر، تابع مربوطه کیت توسعه‌دهندگان را فراخوانی نمایید.

```C#
Sibche.FetchActivePackages((bool isSuccessful, SibcheError error, List<SibchePurchasePackage> purchasedPackages) =>
{
    if (isSuccessful)
    {
        // Success block
    } else
    {
        // failure block
        Debug.Log(error.message);
    }
});
```

در پاسخ، کیت توسعه‌دهندگان موفقیت/عدم موفقیت درخواست و نیز آرایه‌ای از بسته‌های خریداری شده‌ی فعال را برمی‌گرداند. توجه نمایید که این آرایه، آرایه‌ای از نوع `SibchePurchasePackage` است.

منظور از بسته‌های فعال، بسته‌هایی هستند که خریداری شده‌اند و هنوز مصرف نشده‌اند و یا تاریخ انقضایشان به اتمام نرسیده است.
تعریف سیبچه از بسته‌های فعال برای هر کدام از نوع بسته‌ها به شرح زیر می‌باشد:

- `SibcheConsumablePackage`: بسته‌هایی که خریداری شده‌اند ولی هنوز مصرف (Consume) نشده‌اند.
- `SibcheNonConsumablePackage`: بسته‌هایی که خریداری شده‌اند. چون این بسته‌ها یکبار خرید هستند، در صورت خرید، به صورت مادام‌العمر فعال هستند.
- `SibcheSubscriptionPackage`: بسته‌هایی که خریداری شده‌اند ولی هنوز از تاریخ انقضای آنها باقی مانده است.

## مصرف کردن بسته‌ها در سمت کلاینت

<aside class="success">
در صورتی که بخواهید بسته‌های قابل مصرف را در سمت کلاینت (درون خود بازی/برنامه) مصرف کنید، بایستی از این روش استفاده کنید. ولی اگر قصد مصرف و اعتبارسنجی سمت سرور داشته باشید، به بخش بعدی مراجعه نمایید.
</aside>

برای مصرف کردن بسته‌های قابل مصرف (Consumable) بایستی شبیه دستور زیر، تابع مربوطه از کیت توسعه‌دهندگان را فراخوانی کنیم:

در پاسخ پس از مشخص شدن وضعیت درخواست، کیت توسعه‌دهندگان callback داده شده را فراخوانی خواهد کرد.
در صورت موفقیت، یعنی بسته مورد نظر با موفقیت مصرف شده و در صورت عدم موفقیت، در مصرف بسته مورد نظر، دچار مشکلی شده‌ایم.

```C#
Sibche.Consume(purchasedPackage.purchasePackageId, (bool isSuccessful, SibcheError error) =>
{
    if (isSuccessful)
    {
        // Success block
    } else
    {
        // failure block
        Debug.Log(error.message);
    }
});
```

## مصرف کردن بسته‌ها در سمت سرور

برای این کار بایستی، کد `purchasePackageId` که بخشی از کلاس `SibchePurchasePackage` می‌باشد را در دست داشته باشید. سپس لینکی به شکل زیر درست کنید و دیتای وریفای را از سرور بگیرید **(آدرس‌ها Case sensitive هستند!)**:

`https://api.sibche.com/sdk/userInAppPurchasePackages/{purchasePackageId}/verifyConsume`

همچنین برای این درخواست، بایستی هدر HTTP با اسم `App-Key` تنظیم نمایید. این کلید از طریق پنل توسعه‌دهندگان به صورت اختصاصی برای هر برنامه قابل دریافت است. اسم این کلید، **کلید سرور** نام دارد و عبارت ۳۰ کاراکتری است.

نمونه کد قابل فراخوانی برای curl به شکل روبرو خواهد بود:

```shell
curl --header "App-Key: YOUR_SERVER_KEY" -X POST \
'https://api.sibche.com/sdk/userInAppPurchasePackages/1/verifyConsume'
```

در جواب، سرور پاسخی با یکی از **HTTP Response Code** های زیر خواهد داد:

- HTTP Status Code 404 (Not Found):
در صورتی که آدرس لینک را اشتباه وارد کرده باشید، یا بسته مورد نظر با purchasePackageId اشتباه وارد شده باشد، یا بسته مورد نظر قبلا مصرف شده باشد این خطا رخ خواهد داد.  
در این صورت، سرور در متن پاسخ، پاسخی شبیه روبرو خواهد داد:

```json
{
  "message" : "your package is already is used or not found",
  "status_code" : 404
}
```

- HTTP Status Code 401 (Unauthorized):
یعنی اینکه کلید سروری که بر روی App-Key تنظیم شده است، غیر معتبر و اشتباه است.
در این صورت، سرور در متن پاسخ، پاسخی شبیه روبرو خواهد داد:

```json
{
  "message" : "app-key is not valid",
  "status_code" : 401
}
```

- HTTP Status Code 202 (Accepted / OK):
در این صورت یعنی، بسته مورد نظر با موفقیت مصرف شد و خرید کاربر، معتبر بوده است.
برای این حالت،‌ سرور در متن پاسخ، هیچ نوشته‌ای بر نخواهد گرداند.
