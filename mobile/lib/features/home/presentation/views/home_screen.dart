import 'package:flutter/material.dart';
import 'package:iconly/iconly.dart';
import 'package:mobile/features/home/presentation/widgets/category_row.dart';
import 'package:mobile/features/home/presentation/widgets/resturants_card.dart';
import 'package:provider/provider.dart';

import '../viewmodels/home_view_model.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Consumer<HomeViewModel>(
      builder: (context, viewModel, _) {
        final header = viewModel.header;

        return Scaffold(
          appBar: AppBar(
            toolbarHeight: 72,
            titleSpacing: 16,
            title: Row(
              children: [
                CircleAvatar(
                  radius: 20,
                  backgroundImage: AssetImage(
                    header?.avatarAssetPath ??
                        'assets/images/profile_image.jpg',
                  ),
                ),
                const SizedBox(width: 12),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Text(
                      header?.name ?? 'Loading...',
                      style: Theme.of(context).textTheme.titleMedium,
                    ),
                    const SizedBox(height: 2),
                    Row(
                      children: [
                        const Icon(IconlyLight.location, size: 16),
                        const SizedBox(width: 4),
                        Text(
                          header?.location ?? 'Loading location...',
                          style: Theme.of(context).textTheme.bodyMedium,
                        ),
                      ],
                    ),
                  ],
                ),
              ],
            ),
          ),
          body: ListView(
            children: [
              // Search bar
              Padding(
                padding: const EdgeInsets.all(10.0),
                child: TextField(
                  decoration: InputDecoration(
                    hintText: 'Search for meals, restaurants...',
                    prefixIcon: const Icon(IconlyLight.search),
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(12),
                      borderSide: BorderSide.none,
                    ),
                    filled: true,
                    fillColor: Colors.grey[200],
                  ),
                ),
              ),

              // Categories
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      "Categories",
                      style: Theme.of(context).textTheme.headlineLarge
                          ?.copyWith(fontWeight: FontWeight.bold),
                    ),

                    TextButton.icon(
                      onPressed: () {},

                      label: Text("See all"),
                      style: TextButton.styleFrom(
                        foregroundColor: Theme.of(context).colorScheme.primary,
                      ),
                    ),
                  ],
                ),
              ),

              // Placeholder for category cards
              CategoryRow(categories: viewModel.categories),

              // Restaurants Card section

              // Restaurants Card section
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Column(
                  children: viewModel.restaurants.map((restaurant) {
                    return Padding(
                      padding: const EdgeInsets.only(bottom: 16),
                      child: RestaurantCard(restaurant: restaurant),
                    );
                  }).toList(),
                ),
              ),
            ],
          ),
        );
      },
    );
  }
}
