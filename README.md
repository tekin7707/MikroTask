# TaskMo

Net 6, Sql Server, RabbitMQ, Redis ve Docker kullanılarak geliştirilmiş bir API projesidir.

##

D O C K E R

Projeyi ayağa kaldırabilmek için Docker dosyaları ayarlanmıştır. Docker klasörüde yml dosyaları mevcut. Sistemde docker kurulu ise Docker-run.bat dosyası tüm ortamı sağlayacaktır. Docker üzerinde Docker adlı bir klasör oluşacak içerisinde sql server ve rabbitmq eklenecektir.

* Açılan command penceresi kapatılırsa gerekli containerleri manual olarak start etmek gerekiyor.

* Docker-run.bat ise eklenen containerları kaldırır.

* rabbitMq için 15672 portundan arayüze erişebiliriz (guest:guest)
  http://localhost:15672


##

REDIS

Movies ve Movies/{id} bilgilerini tutmak için kullanılmaktadır. Comment eklendiğinde Redis Cache güncellenmektedir.


RabbitMq

Email gönderimleri için kuyruk yapısı kullanılmaktadır. Email gönderim isteği sıraya eklenmekte ve daha sonra gönderilmektedir. Mock bir servis oluşturulmuş ve true değerini dönmektedir. Takip edilebilmesi için wwwroot altına emailin gönderim durumu yazılmaktadır.



