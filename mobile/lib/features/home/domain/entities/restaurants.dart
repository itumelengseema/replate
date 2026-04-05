import 'package:mobile/features/home/domain/entities/food_Item.dart';

class Restaurants {
  final String restaurantsName;
  final String restaurantsLogo;
  final String location;
  final double rating;
  final String type;
  final String tradingHours;
  final String description;
  final String bannerImage;
  final String contactNumber;
  final String email;
  final String website;
  final int numberOfReviews;

  final List<FoodItem?> items;

  Restaurants({
    required this.restaurantsName,
    required this.restaurantsLogo,
    required this.location,
    required this.rating,
    required this.type,
    required this.tradingHours,
    required this.description,
    required this.bannerImage,
    required this.contactNumber,
    required this.email,
    required this.website,
    required this.numberOfReviews,
    required this.items,
  });
}
