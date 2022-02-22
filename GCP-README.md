# Создание проекта

Ссылка на создание проекта через cloud shell: [project creation](https://cloud.google.com/resource-manager/docs/creating-managing-projects#gcloud).

Комманда для создания проекта:
```
gcloud projects create PROJECT_ID
```

`PROJECT_ID` - название проекта.
___
# Создание cloud репозиторя

Ссылка на google cloud документацию: [repository managing](https://cloud.google.com/source-repositories/docs/quickstart?hl=en_US&_ga=2.251826126.676791970.1644443656-2048089208.1637833296&_gac=1.187078106.1644148771.Cj0KCQiAgP6PBhDmARIsAPWMq6kdEJaeIuF_8_53ciptJJScPhRySLoT1XfUlqr5wQCrVwM5W0EOrYkaAm04EALw_wcB).

команды выполняемые в cloud shell:
```
gcloud source repos create REPO_NAME
```

`REPO_NAME` - название репозитория

Получени списка репозиториев google cloud:
```
gcloud source repos list
```

команды выполняемые локально `(необходимо установить иструменты разработчика google cloud)`:

```
gcloud source repos clone REPO_NAME
git add .
git commit -m "..."
git push origin master
```
___
# Создание виртуальной машины, с использованием compute engine
```
gcloud compute instances create VM_NAME --image-project=debian-cloud --image-family=debian-11 --zone=europe-central2-a --hostname=HOST_NAME
```
`VM_NAME` - имя виртуальной машины  
`HOST_NAME` - название для хоста виртуальной машины (можно опустить)

Привязка к созданной виртуальной машине тегов http сервера:
```
gcloud compute instances add-tags VM_NAME --tags http-server,https-server
```
___
# Создание SQL Instance

Полезные ссылки:  
[Instance creating](https://cloud.google.com/sql/docs/mysql/create-instance#gcloud)  
[Regions](https://cloud.google.com/sql/docs/mysql/instance-settings#region-values)  
[Database versions](https://cloud.google.com/sql/docs/postgres/db-versions)  

Создание с использованием комманды:
```
gcloud sql instances create INSTANCE_NAME --database-version=POSTGRES_13 --cpu=1 --memory=3840MB --region=europe-central2
```
`INSTANCE_NAME` - название SQL Instance

Создание базы данных в инстансе:
```
gcloud sql databases create DATABASE_NAME --instance=INSTANCE_NAME
```
`DATABASE_NAME` - название базы данных

Обновление пароля пользователя в SQL Instance
```
gcloud sql users set-password postgres --host=INSTANCE_PUBLIC_IP --instance INSTANCE_NAME --password PASSWORD
```
`INSTANCE_PUBLIC_IP` - публичный ip SQL Instance

Добавление ip адресов в список, позволенных устанавливать подключение к базам данных(Необходимо добавить ip рабочей машины и созданной виртуальной машины в compute engine): 
```
gcloud sql instances patch bug-tracker-database-instance --authorized-networks=[ipAddres1,ipAddres2,...]
```
___
# Разворачивание .NET приложения в compute engine

установка и настройка необходимых приложений:
```
sudo apt install git

sudo apt update
sudo apt install wget

sudo apt install nginx
sudo apt install ufw

sudo ufw allow 'Nginx Full'
sudo service nginx start
```
установка .NET 6
```
wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-6.0

sudo dotnet --version
```

Далее стагиваем репозиторий с google cloud repositories и разворачиваем приложение. Необходимо будет также создать конфигурацию для nginx.

___
# Настройка fluentd



___
# Настройка developer portal

Заходим в проекте в разде `Endpoints`, находим developer portal и создаём его. Далее локально запускаем команду:
```
gcloud endpoints services deploy swagger.json
```
где `swagger.json` это файл, который содержит swagger спецификацию сайта. `!!!Важно!!! неожодимо чтобы это была спецификация swagger V2.0 другие developer portal не поддерживает.`