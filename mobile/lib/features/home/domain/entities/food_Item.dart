import 'package:mobile/features/home/domain/enums/item_type.dart';

class FoodItem {
  final String id;
  final String name;
  final double price;
  final int quantity;
  final ItemType type;
  final String? image;
  final String? description;

  FoodItem({
    required this.id,
    required this.name,
    required this.price,
    required this.quantity,
    required this.type,
    this.image,
    this.description,
  });
}
