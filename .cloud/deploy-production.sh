#!/bin/bash

sudo dotnet publish --configuration Release -p:ASPNETCORE_ENVIRONMENT=Production -o:/var/www/bug-tracker-api

sudo cp ./DeployFiles/default /etc/nginx/sites-available/

sudo cp ./DeployFiles/bug-tracker-api.service /etc/systemd/system/

sudo systemctl enable bug-tracker-api.service
sudo systemctl start bug-tracker-api.service
sudo systemctl status bug-tracker-api.service

