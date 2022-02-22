#!/bin/bash

sudo dotnet publish --configuration Release -p:ASPNETCORE_ENVIRONMENT=Production -o:/var/www/E-Diary

sudo cp ./DeployFiles/default /etc/nginx/sites-available/

sudo cp ./DeployFiles/e-diary-api.service /etc/systemd/system/

sudo systemctl enable e-diary-api.service
sudo systemctl start e-diary-api.service
sudo systemctl status e-diary-api.service

