# TaskMo

Net 6, Sql Server, RabbitMQ, Redis ve Docker kullanılarak geliştirilmiş bir API projesidir. 

##

Migration dosyaları oluşturulmuştur.Uygulama 1499 portundan çalışan bir Sql Server beklemektedir. Identity için IdDb ve Movie listesi için MovieDb olamak üzere 2 farklı database oluşturulacaktır.

##

D O C K E R

Projeyi ayağa kaldırabilmek için Docker dosyaları ayarlanmıştır. Docker-MT klasörüde yml dosyaları mevcut. Sistemde docker kurulu ise Docker-run.bat dosyası tüm ortamı sağlayacaktır. Docker üzerinde Docker-MT adlı bir klasör oluşacak içerisinde sql server(1499), redis(6379) ve rabbitmq(5672) eklenecektir.

* Açılan command penceresi kapatılırsa gerekli containerleri manual olarak start etmek gerekiyor.

* Docker-run.bat ise eklenen containerları kaldırır.

* rabbitMq için 15672 portundan arayüze erişebiliriz (guest:guest)
  http://localhost:15672


##

R E D I S

Movies ve Movies/{id} bilgilerini tutmak için kullanılmaktadır. Comment eklendiğinde Movie{id} Redis Cache güncellenmektedir. 

Movie listesinin güncellenmesi ise kuyruk yapısı üzerinden sağlanmaktadır. Yani, Hosted service ile 1 saat arayla themoviedb.org sitesinden film listesi güncellenmektedir. Yeni eklenen film olması durumunda kuyruk yapısına redisi güncelleyecek bir görev ekleniyor. MikroTask.Services.Application.Consumers/UpdateRedisCommandConsumer.cs  dosyasındaki consume methodu rediste tutulan "movielist" anahtarını siliyor ve sonrasında movielist için yapılan ilk istek databaseden çekiliyor.



RabbitMq

Email gönderimleri ve Redis movie listesi güncellemeleri için kuyruk yapısı kullanılmaktadır. MikroTask.Services.Application.Consumers.SendEmailCommandConsumer

Email gönderim isteği sıraya eklenmekte ve daha sonra gönderilmektedir. Mock bir servis oluşturulmuş ve true değerini dönmektedir. Takip edilebilmesi için wwwroot altına emailin gönderim durumu yazılmaktadır.





