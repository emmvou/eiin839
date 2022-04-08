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
