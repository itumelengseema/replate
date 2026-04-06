import 'package:flutter/material.dart';
import 'package:iconly/iconly.dart';
import 'package:mobile/core/theme/app_colors.dart';
import 'package:mobile/features/home/presentation/views/home_screen.dart';
import 'package:provider/provider.dart';

import '../features/explore/presentation/views/explor_screen.dart';
import '../features/favourite/views/favourite_screen.dart';
import 'viewmodels/app_nav_view_model.dart';

class AppBottomNav extends StatelessWidget {
  const AppBottomNav({super.key});

  @override
  Widget build(BuildContext context) {
    return Consumer<AppNavViewModel>(
      builder: (context, navViewModel, _) {
        final pages = [
          const HomeScreen(),
          const ExplorScreen(),
          const Placeholder(),
          const Placeholder(),
          const FavouriteScreen(),
        ];

        return Scaffold(
          extendBody: true,
          body: IndexedStack(index: navViewModel.currentIndex, children: pages),
          bottomNavigationBar: BottomNavigationBar(
            type: BottomNavigationBarType.fixed,
            backgroundColor: Colors.grey[100],
            elevation: 0,
            currentIndex: navViewModel.currentIndex,
            onTap: navViewModel.selectTab,
            selectedItemColor: AppColors.darkGreen,
            unselectedItemColor: AppColors.mediumGray,
            selectedLabelStyle: const TextStyle(color: AppColors.darkGreen),
            unselectedLabelStyle: const TextStyle(color: AppColors.mediumGray),
            items: const [
              BottomNavigationBarItem(
                icon: Icon(IconlyLight.home),
                label: 'Home',
                activeIcon: Icon(IconlyBold.home),
              ),
              BottomNavigationBarItem(
                icon: Icon(IconlyLight.discovery),
                label: 'Explore',
                activeIcon: Icon(IconlyBold.discovery),
              ),
              BottomNavigationBarItem(
                icon: Icon(IconlyLight.location),
                label: 'Shops',
                activeIcon: Icon(IconlyBold.location),
              ),
              BottomNavigationBarItem(
                icon: Icon(IconlyLight.buy),
                label: 'Orders',
                activeIcon: Icon(IconlyBold.buy),
              ),

              BottomNavigationBarItem(
                icon: Icon(IconlyLight.heart),
                label: 'Favourites',
                activeIcon: Icon(IconlyBold.heart),
              ),
            ],
          ),
        );
      },
    );
  }
}
