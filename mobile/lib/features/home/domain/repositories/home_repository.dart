import '../entities/home_header.dart';

abstract class HomeRepository {
  Future<HomeHeader> getHomeHeader();
}
