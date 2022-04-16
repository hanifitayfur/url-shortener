



<span style="color: #AF54FB;font-size: 19px;"><b>Projenin yapısı nasıl?</b></span>

Bu proje uygulamaların bitly tarzı kısa link ihtiyaçlarının karşılanabilmesi için oluşturulmuştur.

1. `Libs` : Proje kullanılabilecek genel kütüphaneleri kapsar.

2. `API` : Projelerinin ortak kullanımına sunulmuş/sunulacak api servisidir.
3. `WebUI` Web uygulamasıdır.
4. `Area.Admin` : Kısa link yönetimi için oluşturulmuş admin panelidir.





<span style="color: #CC0E0E;font-size: 19px;"><b>Ön gereksinimlerim neler?</b></span>

<table>
   <thead>
      <th>İhtiyaçlar</th>

   </thead>
   <tbody>
      <tr>
         <td>
            <li>Visual Studio 2022</li>
            <li>NET 6 SDK</li>
            <li>Mongo DB</li>
         </td>
   </tbody>
</table>

[![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/colored.png)](#table-of-contents)

<span style="color: #0DBA04;font-size: 19px;"><b> Hazırım, nereden başlıyorum?</b></span>

Proje Net 6 MVC alt yapısı kullanılarak geliştirilmektedir. Net 6 için visual studio 2022 gereklidir.

Projenin iki ana branchi bulunmaktadır. : `master`, `development`

* `master` tüm branchlerin toplu kodunu içerir.
* `development` geliştirilmekte olan kodu içerir.

**Projeyi "Clone" etmek için aşağıdaki linki kullanabilirsiniz:**

```bash
https://github.com/hanifitayfur/url-shortener.git
```

[![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/colored.png)](#table-of-contents)

<span style="color: #D6E70A;font-size: 19px;"><b> Veritabanı üzerinde değişiklikleri nasıl yaparım?</b></span>

* Development veri tabanı üzerinde gerekli değişiklikleri kendiniz **NoSQLBooster for MongoDB** aracılığıyla yapabilirsiniz.

<span style="color: #D6E70A;font-size: 14px;"> Nasıl Çalıştırılır?</span>

* Solutionu visual studio 2022 de açın.
* Solution bazında debug ve release mod birlikte olacak şekilde build alınız.
* İstediğiniz projeyi başlangıç projesi olarak ayarlayınız veya birden fazla projeyi seçiniz.
* launchSettings.json dosyasından profilinizi <span style="color: #FF1925">Localhost</span> olarak ayarlayınız.
* Veritabanı bağlantınızı kontrol ettikten sonra uygulamayı çalıştırınız.

<span style="color: #D6E70A;font-size: 14px;"> Hangi tasarım yaklaşımı kullanılıyor?</span>

Her projede code first yaklaşımı kullanılmaktadır. Tablodaki satırları kullanarak hızlıca aksiyon alabilirsiniz.

[![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/colored.png)](#table-of-contents)

