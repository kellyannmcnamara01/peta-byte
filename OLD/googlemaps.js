function initializeMap(){
    var temiskaming = {
        lat: 47.4943,
        lng: -79.6955
    };
    var map = new google.maps.Map(document.getElementById('contact-hp__map'), {
        zoom: 14,
        center: temiskaming,
        draggable: false,
        disableDefaultUI: true,
        styles: [{
              featureType: 'water',
              elementType: 'geometry',
              stylers: [{color: '#9bdffb'}]
            }]
    });
    
    var marker = new google.maps.Marker({
        position: temiskaming,
        map: map
    });
}
    
   
