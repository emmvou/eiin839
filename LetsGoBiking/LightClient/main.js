//sets map browser on scroll click
const primaryMiddle = function(mapBrowserEvent) {
    const pointerEvent = mapBrowserEvent;
    return pointerEvent.originalEvent.button === 1;
}

//draws initial map
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
    }),
    interactions: ol.interaction.defaults({ dragPan: false }).extend([
        new ol.interaction.DragPan({
            condition: function(mapBrowserEvent) {
                return (
                    ol.events.condition.noModifierKeys(mapBrowserEvent) && primaryMiddle(mapBrowserEvent)
                );
            }
        })
    ])
});

//loads contracts
$.get("http://localhost:8733/Design_Time_Addresses/RoutingWithBikes/Service1/rest/Contracts",
    function(data){
        console.log("contracts:", data);
        for(let d in data){
            $("#towns").append("<option value='"+data[d].commercial_name+"'>"+data[d].name+"</option>");
        }
    });

//stores drawn routes
let drawnLayers = [];

//draws the whole route
function drawRoutes() {
    //resets drawn routes
    reset_map_content();
    //gets selected town
    let towns = $("#towns")[0];
    if(towns.value === ""){
        alert("Please select a town");
        return;
    }
    //gets selected start and end
    if(start[0] === undefined || end[0] === undefined){
        alert("Please select a start and an end point");
        return;
    }
    let opt = towns.options[towns.selectedIndex]; //huge vulnerability here
    //draws route
    $.get("http://localhost:8733/Design_Time_Addresses/RoutingWithBikes/Service1/rest/Route?startLat="+start[0][1]+"&startLong="+start[0][0]+"&endLat="+end[0][1]+"&endLong="+end[0][0]+"&contract="+opt.text,
        function(data){
            console.log("api response:",data);
            drawRoute(data["StartToBike"], false);
            drawRoute(data["BikeToBike"], true);
            drawRoute(data["BikeToEnd"], false);
        }).fail(function(data){
            alert("No route found");
        });
}

//stores start and end points and location
let start = [];
let end = [];

//draws start marker on map
function findStart(){
    let start = $("#start").val();
    findLocation(start, true);
}

//draws end marker on map
function findEnd(){
    let end = $("#end").val();
    findLocation(end, false);
}

//finds marker on map corresponding to location
function findLocation(location, isStart){
    $.get("http://localhost:8733/Design_Time_Addresses/RoutingWithBikes/Service1/rest/SearchAddress?location="+location,
        function(data){
            data = JSON.parse(data)["features"][0]["geometry"]["coordinates"];
            // draw on map
            let coords = ol.proj.fromLonLat([data[0], data[1]]);
            drawMarker(coords, isStart);
        });
}

//draws marker on map
function drawMarker(coords, isStart){
    let marker = new ol.Feature({
        geometry: new ol.geom.Point(coords)
    });
    let markerStyle = new ol.style.Style({
        image: new ol.style.Icon(({
            anchor: [0.5, 1],
            anchorXUnits: 'fraction',
            anchorYUnits: 'fraction',
            src: (isStart ? 'https://pngimg.com/uploads/gps/gps_PNG65.png' : 'https://pngimg.com/uploads/gps/gps_PNG12.png'),
            scale: (isStart? [0.01, 0.01] : [0.1, 0.1])
        }))
    });
    marker.setStyle(markerStyle);
    let vectorSource = new ol.source.Vector({
        features: [marker]
    });
    let vectorLayer = new ol.layer.Vector({
        source: vectorSource,
        style: new ol.style.Style({
            fill: new ol.style.Fill({
                color: 'red'
            }),
            stroke: new ol.style.Stroke({
                color: (isStart ? 'red' : 'blue'),
                width: 3
            })
        }),
        zIndex: 15
    });
    map.addLayer(vectorLayer);

    //removes old markers and stores new ones
    let coord = ol.proj.transform(coords, 'EPSG:3857', 'EPSG:4326');
    console.log("marker", coord);
    if(isStart){
        map.removeLayer(start[1]);
        start = [coord, vectorLayer];
    } else {
        map.removeLayer(end[1]);
        end = [coord, vectorLayer];
    }
}

//draws one route
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
    drawnLayers.push(routeLayer);

}

//removes old routes
function reset_map_content(){
    for(let lay of drawnLayers){
        map.removeLayer(lay);
    }
    drawnLayers = [];
}
//removes right click menu
map.addEventListener('contextmenu', event => event.preventDefault());

//set left click to find start and right click to find end
map.on('pointerdown', function (evt) {
    if(evt.originalEvent.button === 0){
        addMarker(evt.coordinate, true);
        evt.preventDefault();
        evt.stopPropagation();
    }
    else if(evt.originalEvent.button === 2){
        addMarker(evt.coordinate, false);
        evt.preventDefault();
        evt.stopPropagation();
    }
    else if(evt.originalEvent.button === 1){
        evt.preventDefault();
        //evt.stopPropagation();
    }
});

//adds marker to map
function addMarker(coordinates, isLeft) {
    drawMarker(coordinates, isLeft);
}
