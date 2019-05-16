Multi container Sample
https://medium.com/front-end-weekly/net-core-web-api-with-docker-compose-postgresql-and-ef-core-21f47351224f

docker build -t webapimaster .
docker build -t webapibackend .

docker run — name webapimaster -p 8000:80 webapimaster:latest
docker run — name webapibackend -p 8000:80 webapibackend:latest

docker-compose

docker-compose build
docker-compose up


docker ps -a [all the containers]

Powershell
docker stop $(docker ps -a -q) [ stop all the containers]

docker rm $(docker ps -a -q) (remove containers)