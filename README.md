
# Veritabanındaki değişiklikleri anlık yakalama 

Asp.NET Core ile SQL Server veritabanındaki değişiklikler SqlTableDependency kütüphanesi aracılığıyla anlık olarak yakalanmakta ve SignalR teknolojisi ile Angular 10 mimarisiyle geliştirdiğimiz client'lar da grafiksel arayüzlere basılmaktadır.


## Kurulum Notları

- ChartExample dosyasının içindeki ChartsClient dosyasını Visual Studio Code ile açın.
- Klasörleri açtıktan sonra Visual Studio Code terminale bağlanın.
- Terminal adresine dikkat edin. Dosya adresi olduğundan emin olun.
- Terminale "ng serve" yazarak Angolar mimarsi ChartsClient'i ayağa kaldırın.
- https://localhost:4200 adresine bağlanabilirsiniz.
- Sonrasında projeyi ayağa kaldırın. Sql de değişik yaptığınızda anlık olarak chart sistemine yansıyacaktir.
  
## Demo

- Veritabanında güncelleme yapıldığında anlık olarak grafiğe yansır.
![image](https://user-images.githubusercontent.com/81421228/155973740-cda32802-38bd-48d3-9c8e-df6f27ab5ab7.png)

  
