curl -X "GET" "http://localhost:52080/Mechanics/GetMechanicPersonalData?pesel=12345678912" -H "accept:  application/json"

curl -X "GET" "https://localhost:52443/Mechanics/GetMechanicPersonalData?pesel=12345678912" -H "accept:  application/json"

curl -X "GET" "http://localhost:52080/Mechanics/GetAssignedRepairsIds?pesel=12345678912" -H "accept: application/json"

curl -X "GET" "https://localhost:52443/Mechanics/GetAssignedRepairsIds?pesel=12345678912" -H "accept: application/json"

curl -X "GET" "http://localhost:52080/Mechanics/GetMechanicData?pesel=12345678912" -H "accept: application/json"

curl -X "GET" "https://localhost:52443/Mechanics/GetMechanicData?pesel=12345678912" -H "accept: application/json"

curl -X "GET" "http://localhost:52080/Mechanics/GetMechanicsData" -H "accept: application/json"

curl -X "GET" "https://localhost:52443/Mechanics/GetMechanicsData" -H "accept: application/json"

pause