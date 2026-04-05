import 'package:flutter/foundation.dart';

import '../../domain/entities/food_Item.dart';
import '../../domain/entities/food_category.dart';
import '../../domain/entities/home_header.dart';
import '../../domain/entities/restaurants.dart';
import '../../domain/enums/item_type.dart';
import '../../domain/repositories/home_repository.dart';

class HomeViewModel extends ChangeNotifier {
  final HomeRepository _repository;

  HomeHeader? _header;
  bool _isLoading = false;
  bool _hasLoaded = false;
  String? _errorMessage;

  HomeViewModel({required HomeRepository repository})
    : _repository = repository;

  HomeHeader? get header => _header;

  bool get isLoading => _isLoading;

  String? get errorMessage => _errorMessage;

  final List<FoodCategory> categories = [
    FoodCategory(name: 'Burgers', image: 'assets/icons/hamburger.svg'),
    FoodCategory(name: 'Pizza', image: 'assets/icons/pizza.svg'),
    FoodCategory(name: 'Sushi', image: 'assets/icons/sushi.svg'),
    FoodCategory(name: 'Desserts', image: 'assets/icons/donut.svg'),
    FoodCategory(name: 'Bakery', image: 'assets/icons/bakery.svg'),
    FoodCategory(name: 'Healthy', image: 'assets/icons/green-salad.svg'),
    FoodCategory(name: 'Chicken', image: 'assets/icons/chicken.svg'),
    FoodCategory(name: 'Fast Food', image: 'assets/icons/fries.svg'),
  ];

  final List<Restaurants> restaurants = [
    Restaurants(
      restaurantsName: "KFC Braamfontein",
      restaurantsLogo: "assets/images/kfc_logo.jpg",
      location: "Johannesburg, Braamfontein",
      rating: 4.5,
      type: "Fast Food",
      tradingHours: "08:00 - 22:00",
      description: "Famous fried chicken and meals",
      bannerImage: "assets/images/kfc_banner.jpg",
      contactNumber: "+27 11 000 0000",
      email: "kfc@replate.com",
      numberOfReviews: 1200,
      website: "www.kfc.co.za",

      items: [
        FoodItem(
          id: "1",
          name: "Chicken Burger",
          price: 45.0,
          quantity: 10,
          type: ItemType.singleItem,
          image: "assets/images/burger.png",
        ),

        FoodItem(
          id: "2",
          name: "Family Meal",
          price: 120.0,
          quantity: 5,
          type: ItemType.meal,
          image: "assets/images/meal.png",
        ),

        FoodItem(
          id: "3",
          name: "Surprise Bag",
          price: 35.0,
          quantity: 3,
          type: ItemType.supriseBag,
          image: "assets/images/bag.png",
        ),
      ],
    ),
    // Restaurants(
    //   restaurantsName: "Tokyo Bites",
    //   restaurantsLogo: "assets/images/sushi_logo.png",
    //   location: "Rosebank, Johannesburg",
    //   rating: 4.7,
    //   type: "Sushi",
    //   tradingHours: "11:00 - 21:00",
    //   description: "Authentic Japanese food",
    //   bannerImage: "assets/images/pizza_palace_banner.jpg",
    //   contactNumber: "+27 11 222 2222",
    //   email: "tokyo@replate.com",
    //   numberOfReviews: 1500,
    //   website: "www.tokyobites.co.za",
    //
    //   items: [
    //     FoodItem(
    //       id: "7",
    //       name: "Salmon Sushi",
    //       price: 80.0,
    //       quantity: 12,
    //       type: ItemType.singleItem,
    //       image: "assets/images/sushi.png",
    //     ),
    //
    //     FoodItem(
    //       id: "8",
    //       name: "Sushi Combo",
    //       price: 160.0,
    //       quantity: 6,
    //       type: ItemType.meal,
    //       image: "assets/images/sushi_combo.png",
    //     ),
    //
    //     FoodItem(
    //       id: "9",
    //       name: "Mystery Sushi Bag",
    //       price: 60.0,
    //       quantity: 3,
    //       type: ItemType.supriseBag,
    //       image: "assets/images/mystery.png",
    //     ),
    //   ],
    // ),
    Restaurants(
      restaurantsName: 'Pizza Palace',
      restaurantsLogo: 'assets/images/pizza_palace_logo.jpg',
      location: '123 Main St, Cityville',
      rating: 4.5,
      type: 'Italian',
      tradingHours: '10:00 AM - 10:00 PM',
      description: 'The best pizza in town with fresh ingredients.',
      bannerImage: 'assets/images/pizza_palace_banner.jpg',
      contactNumber: '555-123-4567',
      email: 'hello@pizzaplace.co.za',
      website: 'www.pizzaplace.co.za',
      numberOfReviews: 120,
      items: [
        FoodItem(
          id: 'pp1',
          name: 'Margherita Pizza',
          price: 8.99,
          quantity: 1,
          type: ItemType.singleItem,
          image: 'assets/images/margherita_pizza.jpg',
          description: 'Classic pizza with tomato sauce and basil.',
        ),
        FoodItem(
          id: 'pp2',
          name: 'Pepperoni Pizza',
          price: 9.99,
          quantity: 1,
          type: ItemType.singleItem,
          image: 'assets/images/pepperoni_pizza.jpg',
          description: 'Spicy pepperoni pizza.',
        ),
        FoodItem(
          id: 'pp3',
          name: 'Surprise Pizza Bag',
          price: 5.99,
          quantity: 1,
          type: ItemType.supriseBag,
          image: 'assets/images/surprise.png',
          description: 'Mystery discounted food bag.',
        ),
      ],
    ),
  ];

  Future<void> loadHomeHeader() async {
    if (_hasLoaded) return;

    _hasLoaded = true;
    _setLoading(true);

    try {
      _header = await _repository.getHomeHeader();
      _errorMessage = null;
    } catch (_) {
      _errorMessage = 'Unable to load home header.';
    } finally {
      _setLoading(false);
    }
  }

  void _setLoading(bool value) {
    _isLoading = value;
    notifyListeners();
  }
}
