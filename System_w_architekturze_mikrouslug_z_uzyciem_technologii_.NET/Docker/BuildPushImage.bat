::docker login -u wosk
:: -p <password>

docker rmi wosk/wosk.autoservice:mechanicsdatausvc-rest

docker build -f ../Wosk.AutoService.MechanicsDataUSvc.Rest/Dockerfile.prod -t wosk/wosk.autoservice:mechanicsdatausvc-rest ..

docker images

docker image ls --filter label=stage=mechanicsdatausvc-rest_build

docker image prune --filter label=stage=mechanicsdatausvc-rest_build --force

docker push wosk/wosk.autoservice:mechanicsdatausvc-rest

docker images

::docker logout

pause
