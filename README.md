# <div dir="rtl">نصب کیت توسعه‌دهندگان برای بازی‌های یونیتی</div>

## <div dir="rtl">نیازمندیهای نصب کیت توسعه دهندگان</div>

1. <div dir="rtl">بارگزاری برنامه در <a href="https://sibche.com/developer">پنل توسعه‌دهندگان سیبچه</a></div>
2. <div dir="rtl">دریافت کلید برنامه از پنل توسعه‌دهندگان</div>
3. <div dir="rtl">آخرین نسخه‌ی Xcode</div>
4. <div dir="rtl">داشتن unity v2019.1 به بالا</div>

## <div dir="rtl">نصب کیت توسعه‌دهندگان</div>

<div dir="rtl">کیت توسعه‌دهندگان سیبچه مختص یونیتی را می‌توانید از <a href="https://github.com/sibche/SibcheStoreKit-Unity/releases/latest/download/SibcheStoreKit.unitypackage">اینجا</a> دانلود کرده و به پروژه خود اضافه کنید.</div>

## <div dir="rtl">تنظیمات اولیه</div>

<div dir="rtl">
تنظیمات مورد نیاز پروژه به صورت خودکار داخل پروژه در زیربخش 
`Editor`
 اضافه شده است که فایلی با نام
`SibcheBuildPostProcessor.cs`
می‌باشد.
</div>


<div dir="rtl">این فایل شامل دو بخش تنظیم می‌باشد که یکی برای اضافه کردن scheme یا همان url اختصاصی برای بازی شما می‌باشد و بخش دیگر، برای اضافه کردن فایل پروژه به صورت داینامیک به پروژه می‌باشد. در مورد هر دو بخش به تفضیل توضیح خواهیم داد. </div>

### <div dir="rtl">افزودن Scheme مختص برنامه شما</div>

<div dir="rtl">
اگر برنامه شما دارای scheme یا url اختصاصی هست، بایستی این تنظیم را از بخش
`SibcheBuildPostProcessor.cs`
غیرفعال (کامنت) کنید.
</div>

<div dir="rtl">همانند کد زیر می‌توانید url اختصاصی خود را بر روی پروژه تنظیم نمایید.</div>

```C#
AddCustomUrlScheme(path, "testapp", "test");
```

<div dir="rtl">
به جای test، نام دلخواهی را تنظیم کرده و به جای testapp بایستی scheme مورد نظرتان را وارد نمایید. به عنوان مثال scheme تنظیم شده **تلگرام** `tg` و scheme تنظیم شده برای برنامه **اینستاگرام** `instagram` میباشد. لیست کامل scheme برنامه‌های معروف را میتوانید از
<a href="https://ios.gadgethacks.com/news/always-updated-list-ios-app-url-scheme-names-0184033/">اینجا</a>
مشاهده نمایید.
</div>

<div dir="rtl">
توصیه اکید می‌شود از scheme استفاده کنید که مختص شما باشد و ترجیحا طولانی باشد. توجه فرمایید که اگر با برنامه‌های دیگر در تداخل باشد، در فرآیند پرداخت دچار مشکل خواهید شد.
</div>

### <div dir="rtl">افزودن فایل .framework به عنوان کتابخانه دینامیک (embedded framework)</div>

<div dir="rtl">این بخش نیز کلیه تنظیمات مربوط به دینامیک کردن پروژه را انجام داده و کافیست اجازه دهید به همان شکلی که هست باقی بماند:</div>

```C#
AddSibcheFrameworkAsEmbed(path);
```


### <div dir="rtl">افزودن init اولیه کتابخانه سیبچه</div>

<div dir="rtl">در جایی از ورودی کلاس اصلی (تابع Start) بایستی تابع init کتابخانه سیبچه را فراخوانی نمایید تا برای استفاده‌های آتی آماده شده باشد:</div>

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

<div dir="rtl"> به جای `YOUR_API_KEY` بایستی api key دریافت شده از پنل دولوپری سیبچه را وارد نمایید. همچنین به جای `YOUR_SCHEME‍` بایستی scheme تعریف شده از مراحل قبلی را وارد نمایید.</div>

## <div dir="rtl">کلاس‌های مورد استفاده در کیت توسعه‌دهندگان</div>

<div dir="rtl">برای خرید، فعال‌سازی و استفاده از بسته‌های سیبچه، کلاسهای زیر ایجاد شده است که در ادامه در مورد هر کدام توضیحاتی ارائه خواهیم کرد.</div>

- <div dir="rtl">`SibchePackage`: این کلاس، نمایانگر بسته قابل خریدی هست که داخل پنل توسعه‌دهندگان اقدام به تعریفشان کرده‌اید. </div>

- <div dir="rtl">`SibchePurchasePackage`: این کلاس، نمایانگر خرید متناظری از کاربر هست که به یک بسته SibchePackage اختصاص یافته است. این کلاس، شامل اطلاعات خرید، زمان خرید، و تاریخ انقضا و ... خواهد بود.</div>

- <div dir="rtl">`SibcheError`:این کلاس، در مواقع رخداد خطا به شما ارجاع داده خواهد شد که شامل اطلاعات بیشتر از خطاهای رخ داده است. اطلاعاتی از قبیل خطا و Http status code و ...</div>
 
 
### <div dir="rtl">SibchePackage</div>
<div dir="rtl">سه نوع بسته قابل خرید داریم که عبارتند از:</div>

- <div dir="rtl">
  بسته‌های قابل خرید مصرفی یا `SibcheConsumablePackage`:
   بسته‌هایی که قابل مصرف هستند که خریداری شده و داخل بازی یا برنامه مصرف می‌شود. مانند بسته‌ی ۵۰۰ سکه طلا یا شارژ قابل مصرف داخل برنامه.

این بسته ها تا زمانی که مصرف نشده‌اند، فعال و معتبر می‌باشند و پس از مصرف، قابل خرید مجدد هستند. 
</div>

- <div dir="rtl">
  بسته‌های قابل خرید غیر مصرفی یا `SibcheNonConsumablePackage`:
  بسته‌هایی که قابل مصرف نیستند و فقط یکبار خریداری می‌شوند. مانند بسته‌ی باز شدن قابلیت آپلود آواتار یا تغییر نام.

این بسته ها، فقط یکبار قابل خرید هستند و پس از خرید، همواره در لیست بسته‌های فعال کاربر خواهند بود.
</div>


- <div dir="rtl">
  بسته‌های اشتراک یا `SibcheSubscriptionPackage`:
  بسته‌هایی هستند که به مدت محدود قابل استفاده بوده و پس از اتمام زمان آنها، کاربر مجاز به استفاده از آن قابلیت نیست. به عنوان مثال، قابلیت استفاده از امکانات ویژه به مدت یک سال

همه این سه کلاس از کلاس والد `SibchePackage` گرفته شده‌اند و به صورت عمومی شامل خصوصیات زیر هستند.
</div>


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

<div dir="rtl">علاوه بر این توابع، کلاس `SibcheSubscriptionPackage` شامل خصوصیات اضافی‌تر زیر نیز هست:</div>

```C#
public int duration;
public string group;
```

### <div dir="rtl">SibchePurchasePackage</div>

<div dir="rtl">این کلاس، شامل تناظر خرید کاربر به بسته‌های شما می‌باشد. این کلاس شامل خصوصیات زیر است:</div>


```C#
public string purchasePackageId;
public string type;
public string code;
public DateTime expireAt;
public DateTime createdAt;
public SibchePackage package;
```

### <div dir="rtl">SibcheError</div>

<div dir="rtl">این کلاس در مواقع بروز خطا به شما داده خواهد شد و شامل خصوصیات زیر می‌باشد:</div>

```C#
public string message;
public int errorCode;
public int statusCode;
```

- <div dir="rtl">errorCode: همان شماره خطایی هست که مطابق جدول زیر ایجاد شده است.</div>
- <div dir="rtl">message: پیغام خطایی هست که از طرف سرور دریافت شده است.</div>
- <div dir="rtl">statusCode:  همان شماره خطای http هست که سرور در جواب درخواست ما داده است.</div>

| شماره errorCode | دلیل خطای مربوطه                         |
| --------------- | ---------------------------------------- |
| 1000            | خطای نامشخص                              |
| 1001            | این بسته قبلا خریداری شده است            |
| 1002            | کاربر از ادامه عملیات منصرف شد                 |
| 1003            | در فرایند ورود (لاگین) دچار مشکل شده‌ایم |
| 1004            |  برنامه به درستی initiate نشده است |

## <div dir="rtl">گرفتن لیست بسته‌های قابل خرید</div>

<div dir="rtl">پس از تنظیم برنامه، میتوانید بسته‌های قابل خرید را مشاهده نمایید. کافیست همانند کد زیر، اقدام به فراخوانی تابع مورد نظر نمایید:</div>

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

<div dir="rtl">در پاسخ، در صورت موفقیت، کیت توسعه‌دهندگان بسته‌های قابل خرید را به عنوان پارامتر پاسخ به شما تحویل میدهد. این پارامتر آرایه‌ای از بسته‌های قابل خرید می‌باشد. این بسته‌ها از نوع `SibchePackage` هستند. </div>

<div dir="rtl">در صورت ناموفق بودن درخواست، پارمتری با نام error از نوع `‍‍SibcheError` ارسال می‌شود، وگرنه به صورت null داده خواهد شد.</div>

## <div dir="rtl">گرفتن اطلاعات بسته مشخص</div>

<div dir="rtl">با در اختیار داشتن آیدی یا کد باندل بسته مورد نظر می‌توانید اطلاعات آن بسته را در اختیار بگیرید. نحوه استفاده از این API به شکل زیر است:</div>


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

<div dir="rtl">پارامتر اول داده شده، همان callback ارسال شده ما است که پس از مشخص شدن وضعیت درخواست، فراخوانی خواهد شد. در صورت موفقیت، بسته‌ی مورد نظر در قالب آبجکت `SibchePackage` (بسته به نوع بسته) به شما ارسال خواهد شد.</div>


## <div dir="rtl">خرید بسته مشخص</div>

<div dir="rtl">پس از گرفتن لیست بسته‌ها، میتوانید درخواست خرید این بسته‌ها را از طریق کد روبرو به کیت توسعه‌دهندگان بدهید. در ادامه، کیت توسعه‌دهندگان، در صورت نیاز کاربر را لاگین کرده و فرایند پرداخت را پیگیری خواهد کرد. سپس موفق یا ناموفق بودن خرید را به همراه `SibchePurchasePackage` به اطلاع شما خواهد رساند.</div>


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

## <div dir="rtl">گرفتن لیست بسته های خریداری شده</div>

<div dir="rtl">با استفاده از این دستور، میتوانید لیست بسته‌های فعال (خریداری شده) کاربر را بدست آورید. کافیست همانند کد زیر، تابع مربوطه کیت توسعه‌دهندگان را فراخوانی نمایید.</div>

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

<div dir="rtl">در پاسخ، کیت توسعه‌دهندگان موفقیت/عدم موفقیت درخواست و نیز آرایه‌ای از بسته‌های خریداری شده‌ی فعال را برمی‌گرداند. توجه نمایید که این آرایه، آرایه‌ای از نوع `SibchePurchasePackage` است.</div>

<div dir="rtl">
منظور از بسته‌های فعال، بسته‌هایی هستند که خریداری شده‌اند و هنوز مصرف نشده‌اند و یا تاریخ انقضایشان به اتمام نرسیده است.
تعریف سیبچه از بسته‌های فعال برای هر کدام از نوع بسته‌ها به شرح زیر می‌باشد:
</div>

- <div dir="rtl">`SibcheConsumablePackage`: بسته‌هایی که خریداری شده‌اند ولی هنوز مصرف (Consume) نشده‌اند.</div>
- <div dir="rtl">`SibcheNonConsumablePackage`: بسته‌هایی که خریداری شده‌اند. چون این بسته‌ها یکبار خرید هستند، در صورت خرید، به صورت مادام‌العمر فعال هستند.</div>
- <div dir="rtl">`SibcheSubscriptionPackage`: بسته‌هایی که خریداری شده‌اند ولی هنوز از تاریخ انقضای آنها باقی مانده است.</div>

## <div dir="rtl">مصرف کردن بسته‌ها در سمت کلاینت</div>

<div dir="rtl">
در صورتی که بخواهید بسته‌های قابل مصرف را در سمت کلاینت (درون خود بازی/برنامه) مصرف کنید، بایستی از این روش استفاده کنید. ولی اگر قصد مصرف و اعتبارسنجی سمت سرور داشته باشید، به بخش بعدی مراجعه نمایید.
</div>

<div dir="rtl">برای مصرف کردن بسته‌های قابل مصرف (Consumable) بایستی شبیه دستور زیر، تابع مربوطه از کیت توسعه‌دهندگان را فراخوانی کنیم:</div>

<div dir="rtl">
در پاسخ پس از مشخص شدن وضعیت درخواست، کیت توسعه‌دهندگان callback داده شده را فراخوانی خواهد کرد.
در صورت موفقیت، یعنی بسته مورد نظر با موفقیت مصرف شده و در صورت عدم موفقیت، در مصرف بسته مورد نظر، دچار مشکلی شده‌ایم.
</div>

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

## <div dir="rtl">مصرف کردن بسته‌ها در سمت سرور</div>

<div dir="rtl">برای این کار بایستی، کد `purchasePackageId` که بخشی از کلاس `SibchePurchasePackage` می‌باشد را در دست داشته باشید. سپس لینکی به شکل زیر درست کنید و دیتای وریفای را از سرور بگیرید **(آدرس‌ها Case sensitive هستند!)**:</div>

`https://api.sibche.com/sdk/userInAppPurchasePackages/{purchasePackageId}/verifyConsume`

<div dir="rtl">همچنین برای این درخواست، بایستی هدر HTTP با اسم `App-Key` تنظیم نمایید. این کلید از طریق پنل توسعه‌دهندگان به صورت اختصاصی برای هر برنامه قابل دریافت است. اسم این کلید، **کلید سرور** نام دارد و عبارت ۳۰ کاراکتری است.
</div>

<div dir="rtl">نمونه کد قابل فراخوانی برای curl به شکل زیر خواهد بود:</div>

```shell
curl --header "App-Key: YOUR_SERVER_KEY" -X POST \
'https://api.sibche.com/sdk/userInAppPurchasePackages/1/verifyConsume'
```

<div dir="rtl">در جواب، سرور پاسخی با یکی از **HTTP Response Code** های زیر خواهد داد:</div>

- <div dir="rtl">HTTP Status Code 404 (Not Found):</div>
<div dir="rtl">
در صورتی که آدرس لینک را اشتباه وارد کرده باشید، یا بسته مورد نظر با purchasePackageId اشتباه وارد شده باشد، یا بسته مورد نظر قبلا مصرف شده باشد این خطا رخ خواهد داد.  
در این صورت، سرور در متن پاسخ، پاسخی شبیه روبرو خواهد داد:
</div>

```json
{
  "message" : "your package is already is used or not found",
  "status_code" : 404
}
```

- <div dir="rtl">HTTP Status Code 401 (Unauthorized):</div>
<div dir="rtl">
یعنی اینکه کلید سروری که بر روی App-Key تنظیم شده است، غیر معتبر و اشتباه است.
در این صورت، سرور در متن پاسخ، پاسخی شبیه روبرو خواهد داد:
</div>

```json
{
  "message" : "app-key is not valid",
  "status_code" : 401
}
```

- <div dir="rtl">HTTP Status Code 202 (Accepted / OK):</div>

<div dir="rtl">
در این صورت یعنی، بسته مورد نظر با موفقیت مصرف شد و خرید کاربر، معتبر بوده است.
برای این حالت،‌ سرور در متن پاسخ، هیچ نوشته‌ای بر نخواهد گرداند.
</div>
