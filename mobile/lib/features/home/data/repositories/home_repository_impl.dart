import '../../domain/entities/home_header.dart';
import '../../domain/repositories/home_repository.dart';

class HomeRepositoryImpl implements HomeRepository {
  @override
  Future<HomeHeader> getHomeHeader() async {
    return const HomeHeader(
      name: 'Itumeleng Seema',
      location: 'Gauteng, Pretoria',
      avatarAssetPath: 'assets/images/profile_image.jpg',
    );
  }
}
