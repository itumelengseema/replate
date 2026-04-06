import 'package:flutter/material.dart';
import 'package:iconly/iconly.dart';

class StoresBottomSheet extends StatelessWidget {
  final ScrollController controller;

  const StoresBottomSheet({super.key, required this.controller});

  @override
  Widget build(BuildContext context) {
    final titleStyle =
        Theme.of(context).textTheme.titleLarge ??
        Theme.of(context).textTheme.titleMedium ??
        const TextStyle(fontSize: 18, fontWeight: FontWeight.bold);

    return Container(
      decoration: const BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.vertical(top: Radius.circular(20)),
      ),
      // drag handle icon
      child: ListView(
        controller: controller,
        children: [
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 10.0),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.start,
              children: [
                Icon(IconlyLight.location, size: 24),
                const SizedBox(width: 8),

                Text('Nearby Stores', style: titleStyle),
              ],
            ),
          ),
          Divider(
            color: Colors.grey[300],
            thickness: 1,
            indent: 10,
            endIndent: 5,
          ),
          const SizedBox(height: 10),
          ListTile(
            leading: const CircleAvatar(
              backgroundImage: AssetImage('assets/images/kfc_logo.jpg'),
            ),
            title: const Text('KFC Braamfontain'),
            subtitle: const Text('0.5 miles away'),
            trailing: Row(
              mainAxisSize: MainAxisSize.min,
              children: const [
                Icon(Icons.star, color: Colors.orange, size: 16),
                SizedBox(width: 4),
                Text('4.5'),
                SizedBox(width: 8),
                Icon(Icons.arrow_forward_ios, size: 18),
              ],
            ),
            onTap: () {
              // Handle store tap
            },
          ),
          ListTile(
            leading: const CircleAvatar(
              backgroundImage: AssetImage('assets/images/store2.jpg'),
            ),
            title: const Text('Sushi Spot'),
            subtitle: const Text('0.8 miles away'),
            trailing: const Icon(Icons.arrow_forward_ios, size: 18),
            onTap: () {
              // Handle store tap
            },
          ),
          ListTile(
            leading: const CircleAvatar(
              backgroundImage: AssetImage('assets/images/store3.jpg'),
            ),
            title: const Text('Burger Joint'),
            subtitle: const Text('1.2 miles away'),
            trailing: const Icon(Icons.arrow_forward_ios, size: 18),
            onTap: () {
              // Handle store tap
            },
          ),
        ],
      ),
    );
  }
}
