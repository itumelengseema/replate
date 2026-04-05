import 'package:flutter/foundation.dart';

import '../../domain/entities/home_header.dart';
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

  Future<void> loadHomeHeader() async {
    if (_hasLoaded) {
      return;
    }

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
