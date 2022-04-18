function test(){
    let div = document.getElementById("test");
    div.classList.add("test");
    div.appendChild(document.createTextNode("Hello World"));
}

//"Add?x={value1}&y={value2}"
//http://localhost:8733/Design_Time_Addresses/RoutingWithBikes/Service1/
function testConnect(){
    let request = new XMLHttpRequest();
    request.open("get", "http://localhost:8733/Design_Time_Addresses/RoutingWithBikes/Service1/rest/Add?x=1&y=2", true);
    request.setRequestHeader("accept", "application/json");
    request.onload = function(){
        document.getElementById("testCon").appendChild(document.createTextNode("ÇA MARCHE"));
        let cts = JSON.parse(this.responseText);
        document.getElementById("testCon").appendChild(document.createTextNode("1+2="+cts["AddResult"]));
        console.log("1+2="+cts["AddResult"]);
    };
    request.onerror = function(e){
        console.log("error");
        document.getElementById("testCon").appendChild(document.createTextNode("error sending request"));
        console.log(e);
        console.log(this);
    };
    request.send();
}

let map = new ol.Map({
    target: 'map',
    layers: [
        new ol.layer.Tile({
            source: new ol.source.OSM()
        })
    ],
    view: new ol.View({
        center: ol.proj.fromLonLat([37.41, 8.82]),
        zoom: 4
    })
});

function drawTest() {
    // route returns an array of 3 routes
    $.get("http://localhost:8733/Design_Time_Addresses/RoutingWithBikes/Service1/rest/Route?startLat=45.774200&startLong=4.867518&endLat=45.75190&endLong=4.821669&contract=lyon",
        function(data){
            console.log(data);
            let route = data["RouteResult"];
            let routeCoords = [];
            for(let i = 0; i < route.length; i++){
                routeCoords.push(ol.proj.fromLonLat([route[i]["Longitude"], route[i]["Latitude"]]));
            }
            let routeLine = new ol.Feature({
                geometry: new ol.geom.LineString(routeCoords)
            });
            let routeSource = new ol.source.Vector({
                features: [routeLine]
            });
            let routeLayer = new ol.layer.Vector({
                source: routeSource
            });
            map.addLayer(routeLayer);
        });

}
