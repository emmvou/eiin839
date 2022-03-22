document.getElementById("workingTest").innerHTML="STATIONS JCDECAUX";

function retrieveAllContracts(){
    let request = new XMLHttpRequest();
    request.open("get", "https://api.jcdecaux.com/vls/v3/contracts?apiKey=f51f7e272f7aa2c0b30e9e3f6d6d3ea8fa202c8e");
    request.setRequestHeader("Accept", "application/json");
    request.onload = function(){
        console.log(this.responseText);
        let cts = JSON.parse(this.responseText);
        let select = document.getElementById("contract");
        for (let {name} of cts){
            let option = document.createElement("option");
            option.value = name;
            option.innerHTML = name;
            select.appendChild(option);
        }
    };
    request.send();
}
let stations;
function retrieveContractStations(){
    let request = new XMLHttpRequest();
    request.open("get", "https://api.jcdecaux.com/vls/v3/stations?contract=" + document.getElementById("contract").value + "&apiKey=f51f7e272f7aa2c0b30e9e3f6d6d3ea8fa202c8e");
    request.setRequestHeader("Accept", "application/json");
    request.onload = function(){
        console.log(this.responseText);
        let cts = JSON.parse(this.responseText);
        stations = cts;
        let select = document.getElementById("station");
        for (let {number, name} of cts){
            let option = document.createElement("option");
            option.value = number;
            option.innerHTML = name;
            select.appendChild(option);
        }
    };
    request.send();
}

function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2) {
    // Radius of the earth in km
    let earthRadius = 6371;
    let dLat = deg2rad(lat2-lat1);
    let dLon = deg2rad(lon2-lon1);
    let a =
        Math.sin(dLat/2) * Math.sin(dLat/2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon/2) * Math.sin(dLon/2);

    let c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    return earthRadius * c; // Distance in km
}

function deg2rad(deg) {
    return deg * (Math.PI/180)
}

function findNearestStation()
{
    let latitude = document.getElementById("latitude").value;
    let longitude = document.getElementById("longitude").value;
    let minDistance = 1 / 0;
    let nearestStation;
    for (let station of stations){
        let distance = getDistanceFrom2GpsCoordinates(latitude, longitude, station.position.latitude, station.position.longitude);
        if (distance < minDistance){
            minDistance = distance;
            nearestStation = station;
        }
    }
    document.getElementById("nearestStation").innerHTML = nearestStation.name;
}
