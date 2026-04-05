import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

import '../../domain/entities/food_category.dart';

class CategoryRow extends StatelessWidget {
  final List<FoodCategory> categories;

  const CategoryRow({super.key, required this.categories});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 100,
      child: ListView.separated(
        scrollDirection: Axis.horizontal,
        itemCount: categories.length,
        separatorBuilder: (_, index) => const SizedBox(width: 12),
        itemBuilder: (context, index) {
          final category = categories[index];

          return Padding(
            padding: const EdgeInsets.symmetric(horizontal: 10.0),
            child: Column(
              children: [
                Container(
                  width: 65,
                  height: 65,
                  decoration: BoxDecoration(
                    color: Colors.transparent,
                    borderRadius: BorderRadius.circular(18),
                  ),
                  child: Padding(
                    padding: const EdgeInsets.all(12),
                    child: category.image.toLowerCase().endsWith('.svg')
                        ? SvgPicture.asset(category.image, fit: BoxFit.contain)
                        : Image.asset(category.image, fit: BoxFit.contain),
                  ),
                ),

                const SizedBox(height: 6),

                Text(
                  category.name,
                  style: Theme.of(context).textTheme.bodyMedium,
                ),
              ],
            ),
          );
        },
      ),
    );
  }
}
