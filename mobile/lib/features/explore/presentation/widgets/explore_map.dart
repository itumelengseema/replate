import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

class ExploreMap extends StatelessWidget {
  final CameraPosition initialPosition;
  final Set<Marker> markers;
  final Function(GoogleMapController) onMapCreated;

  const ExploreMap({
    super.key,
    required this.initialPosition,
    required this.markers,
    required this.onMapCreated,
  });

  @override
  Widget build(BuildContext context) {
    return GoogleMap(
      initialCameraPosition: initialPosition,
      markers: markers,
      onMapCreated: onMapCreated,
      myLocationButtonEnabled: true,
      zoomControlsEnabled: false,
    );
  }
}
