import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../viewmodels/explor_view_model.dart';
import '../widgets/explore_map.dart';
import '../widgets/search_bar.dart' as explore_widgets;
import '../widgets/stores_bottom_sheet.dart';

class ExplorScreen extends StatelessWidget {
  const ExplorScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Consumer<ExplorViewModel>(
      builder: (context, viewModel, _) {
        return Scaffold(
          body: Stack(
            children: [
              ExploreMap(
                initialPosition:
                    viewModel.intialPosition, // matches your VM name
                markers: viewModel.markers,
                onMapCreated: viewModel.onMapCreated,
              ),
              const Positioned(
                top: 50,
                left: 16,
                right: 16,
                child: explore_widgets.SearchBar(),
              ),

              // DraggableScrollableSheet(
              DraggableScrollableSheet(
                initialChildSize: 0.2,
                minChildSize: 0.1,
                maxChildSize: 0.6,

                builder: (context, scrollController) {
                  return StoresBottomSheet(controller: scrollController);
                },
              ),
            ],
          ),
        );
      },
    );
  }
}
