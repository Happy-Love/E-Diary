#!/bin/bash

sudo apt update

sudo apt install wget

wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-6.0

echo The current version of dotnet is:
sudo dotnet --version


sudo apt install nginx
sudo apt install ufw

sudo ufw allow 'Nginx Full'
sudo service nginx start
