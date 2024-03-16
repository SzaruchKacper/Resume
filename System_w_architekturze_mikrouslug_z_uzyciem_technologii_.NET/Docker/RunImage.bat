::docker login -u wosk

docker ps -a

docker stop mechanicsdatausvc

docker rm mechanicsdatausvc

docker ps 

docker images

docker pull wosk/wosk.autoservice:mechanicsdatausvc-rest

docker run --name mechanicsdatausvc -p 52080:80 -id wosk/wosk.autoservice:mechanicsdatausvc-rest

pause

::docker stop mechanicsdatausvc

::docker rm mechanicsdatausvc

::pause
