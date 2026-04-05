import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../viewmodels/explor_view_model.dart';

class ExplorScreen extends StatelessWidget {
  const ExplorScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Consumer<ExplorViewModel>(
      builder: (context, viewModel, _) {
        return Scaffold(
          body: Center(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Text(
                  viewModel.title,
                  style: Theme.of(context).textTheme.titleMedium,
                ),
                const SizedBox(height: 8),
                Text(
                  viewModel.description,
                  style: Theme.of(context).textTheme.bodyMedium,
                  textAlign: TextAlign.center,
                ),
              ],
            ),
          ),
        );
      },
    );
  }
}
