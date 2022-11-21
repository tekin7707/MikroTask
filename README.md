# TaskMo

Net 6, Sql Server, RabbitMQ, Docker kullanılarak geliştirilmiş bir API projesidir.

##

D O C K E R

Projeyi ayağa kaldırabilmek için Docker dosyaları ayarlanmıştır. Docker klasörüde yml dosyaları mevcut. Sistemde docker kurulu ise Docker-run.bat dosyası tüm ortamı sağlayacaktır. Docker üzerinde Docker adlı bir klasör oluşacak içerisinde sql server ve rabbitmq eklenecektir.

* Açılan command penceresi kapatılırsa gerekli containerleri manual olarak start etmek gerekiyor.

* Docker-run.bat ise eklenen containerları kaldırır.

* rabbitMq için 15672 portundan arayüze erişebiliriz (guest:guest)
  http://localhost:15672


