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
        center: ol.proj.fromLonLat([3.0, 47.0]),
        zoom: 6
    })
});

$.get("http://localhost:8733/Design_Time_Addresses/RoutingWithBikes/Service1/rest/Contracts",
    function(data){
        console.log(data);
        for(d in data){
            $("#towns").append("<option value='"+data[d].commercial_name+"'>"+data[d].name+"</option>");
        }
    });


function drawTest() {
    let towns = $("#towns")[0];
    if(towns.value === ""){
        alert("Please select a town");
        return;
    }
    let opt = towns.options[towns.selectedIndex]; //huge vulnerability here
    $.get("http://localhost:8733/Design_Time_Addresses/RoutingWithBikes/Service1/rest/Route?startLat=45.784300&startLong=4.867518&endLat=45.751&endLong=4.681669&contract="+opt.text,
        function(data){
            console.log(data);
            drawRoute(data["StartToBike"], false);
            drawRoute(data["BikeToBike"], true);
            drawRoute(data["BikeToEnd"], false);
        });
}

function drawRoute(route, isBike){
    route = JSON.parse(route);
    const points = new ol.format.GeoJSON().readFeatures(route, {
        dataProjection: 'EPSG:4326',
        featureProjection: 'EPSG:3857',
    });
    let routeCoords = [];
    routeCoords.push(ol.proj.fromLonLat([route["Longitude"], route["Latitude"]]));

    let routeSource = new ol.source.Vector({
        features: points,
    });
    let routeLayer = new ol.layer.Vector({
        source: routeSource,
        style: new ol.style.Style({
            fill: new ol.style.Fill({
                color: 'red'
            }),
            stroke: new ol.style.Stroke({
                color: (isBike? 'red' : 'blue'),
                width: 3
            })
        }),
        zIndex: 10
    });
    map.addLayer(routeLayer);
    console.log("drawn");
    console.log(routeLayer);

}
