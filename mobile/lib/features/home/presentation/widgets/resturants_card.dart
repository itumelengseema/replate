import 'package:flutter/material.dart';
import 'package:mobile/features/home/domain/entities/restaurants.dart';
import 'package:mobile/features/home/domain/enums/item_type.dart';

class RestaurantCard extends StatelessWidget {
  final Restaurants restaurant;

  const RestaurantCard({super.key, required this.restaurant});

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(bottom: 16),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(20),
        color: Colors.white,
        boxShadow: [
          BoxShadow(
            color: Colors.black.withValues(alpha: 0.05),
            blurRadius: 10,
            offset: const Offset(0, 4),
          ),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // 🖼️ Banner Image
          ClipRRect(
            borderRadius: const BorderRadius.vertical(top: Radius.circular(20)),
            child: Image.asset(
              restaurant.bannerImage,
              height: 150,
              width: double.infinity,
              fit: BoxFit.cover,
            ),
          ),

          Padding(
            padding: const EdgeInsets.all(12),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // 🏪 Name + Rating
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    // Restaurant logo
                    CircleAvatar(
                      radius: 20,
                      backgroundImage: AssetImage(restaurant.restaurantsLogo),
                    ),
                    SizedBox(width: 10),
                    Expanded(
                      child: Text(
                        restaurant.restaurantsName,
                        style: const TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                    Row(
                      children: [
                        const Icon(Icons.star, color: Colors.orange, size: 18),
                        const SizedBox(width: 4),
                        Text(restaurant.rating.toString()),
                      ],
                    ),
                  ],
                ),

                const SizedBox(height: 6),

                // 📍 Location
                Text(
                  restaurant.location,
                  style: TextStyle(color: Colors.grey[600], fontSize: 12),
                ),

                const SizedBox(height: 10),

                // 🧠 Tags (Surprise bag indicator)
                Wrap(
                  spacing: 8,
                  children: [
                    _chip(restaurant.type),

                    if (restaurant.items.any(
                      (item) => item?.type == ItemType.supriseBag,
                    ))
                      _chip("Surprise Bags Available 🛍️"),
                  ],
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }

  Widget _chip(String label) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 6),
      decoration: BoxDecoration(
        color: Colors.green.withValues(alpha: 0.1),
        borderRadius: BorderRadius.circular(20),
      ),
      child: Text(
        label,
        style: const TextStyle(
          fontSize: 12,
          color: Colors.green,
          fontWeight: FontWeight.w500,
        ),
      ),
    );
  }
}
