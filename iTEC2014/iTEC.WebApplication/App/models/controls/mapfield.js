define(['models/controls/control', 'models/controls/textfield', 'models/controls/addontextfield'],
    function (control, textfield, addontextfield) {

        var model = function (options) {

            options = options || {};

            var self = this;

            control.call(self, options);

            var map = undefined;
            var marker = undefined;

            self.address = new addontextfield({ text: 'Address', value: options.address, icon: 'fa-map-marker', action: { icon: 'fa-crosshairs', click: refreshAddress } });
            //self.address.value.subscribe(function (value) {
            //    if (!self.location()) {
            //        var geocoder = new google.maps.Geocoder();
                    
            //        geocoder.geocode({ address: self.address.value() }, function (results, status) {
            //            if (status == google.maps.GeocoderStatus.OK) {
            //                self.location(results[0].geometry.location);
            //                map.panTo(results[0].geometry.location);
            //            }
            //        });
            //    }
            //});
            self.location = ko.observable();
            self.location.subscribe(function (value) {
                var location = new google.maps.LatLng(value.latitude, value.longitude);
                if (marker) {
                    marker.setPosition(location);
                } else {
                    marker = new google.maps.Marker({
                        position: location,
                        animation: google.maps.Animation.DROP,
                        map: map,
                        draggable: true
                    });

                    google.maps.event.addListener(marker, 'dragend', function (event) {
                        self.location({
                            latitude: event.latLng.lat(),
                            longitude: event.latLng.lng()
                        });
                    });
                }
                map.panTo(location);
            });

            self.initialize = function (m) {
                map = m;
                setCurrentPosition();

                google.maps.event.addListener(map, 'click', function (event) {
                    self.location({
                        latitude: event.latLng.lat(),
                        longitude: event.latLng.lng()
                    });
                });
            };

            function refreshAddress() {
                var location = new google.maps.LatLng(self.location().latitude, self.location().longitude);
                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({ latLng: location }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        var lat = results[0].geometry.location.lat(),
                            lng = results[0].geometry.location.lng(),
                            placeName = results[0].address_components[0].long_name,
                            latlng = new google.maps.LatLng(lat, lng);

                        self.address.value(results[0].formatted_address);
                    }
                });
            }

            function setCurrentPosition() {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function (position) {
                        self.location({ 
                            latitude: position.coords.latitude, 
                            longitude: position.coords.longitude
                        });
                        google.maps.event.trigger(map, 'resize');
                    }, function (error) {
                        console.log(error);
                    });
                }
            }

        };

        return model;

    }
);