import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'core/theme/app_theme.dart';
import 'features/explore/presentation/viewmodels/explor_view_model.dart';
import 'features/favourite/presentation/viewmodels/favourite_view_model.dart';
import 'features/home/data/repositories/home_repository_impl.dart';
import 'features/home/presentation/viewmodels/home_view_model.dart';
import 'shared/app_bottom_nav.dart';
import 'shared/viewmodels/app_nav_view_model.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider<AppNavViewModel>(
          create: (_) => AppNavViewModel(),
        ),
        ChangeNotifierProvider<HomeViewModel>(
          create: (_) =>
              HomeViewModel(repository: HomeRepositoryImpl())..loadHomeHeader(),
        ),
        ChangeNotifierProvider<ExplorViewModel>(
          create: (_) => ExplorViewModel(),
        ),
        ChangeNotifierProvider<FavouriteViewModel>(
          create: (_) => FavouriteViewModel(),
        ),
      ],
      child: MaterialApp(
        title: 'Replate',
        theme: AppTheme.lightTheme,
        debugShowCheckedModeBanner: false,
        home: const AppBottomNav(),
      ),
    );
  }
}
