function initializeMap(){
    var temiskaming = {
        lat: 47.4943,
        lng: -79.6955
    };
    var map = new google.maps.Map(document.getElementById('contact-hp__map'), {
        zoom: 8,
        center: temiskaming,
        draggable: false,
        mapTypeControl: false,
    });
    var marker = new google.maps.Marker({
        position: temiskaming,
        map: map
    });
}
