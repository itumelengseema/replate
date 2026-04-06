import 'dart:async';

import 'package:flutter/foundation.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

class ExplorViewModel extends ChangeNotifier {
  final Completer<GoogleMapController> _controller = Completer();

  CameraPosition intialPosition = const CameraPosition(
    target: LatLng(-26.2041, 28.0473), // Johannesburg coordinates
    zoom: 14,
  );

  Set<Marker> markers = {};

  void onMapCreated(GoogleMapController controller) {
    _controller.complete(controller);
  }

  void loadStores() {
    // mock data for stores

    markers = {
      Marker(
        markerId: const MarkerId('store1'),
        position: const LatLng(-26.2041, 28.0473),
        infoWindow: const InfoWindow(title: 'Store 1', snippet: '123 Main St'),
      ),
      Marker(
        markerId: const MarkerId('store2'),
        position: const LatLng(-26.2050, 28.0480),
        infoWindow: const InfoWindow(title: 'Store 2', snippet: '456 Elm St'),
      ),
      Marker(
        markerId: const MarkerId('store3'),
        position: const LatLng(-26.2060, 28.0490),
        infoWindow: const InfoWindow(title: 'Store 3', snippet: '789 Oak St'),
      ),
    };
    notifyListeners();
  }
}
