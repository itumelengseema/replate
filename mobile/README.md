# Replate Mobile App

Replate Mobile is the Flutter client for the Replate platform. It focuses on helping users discover food options, browse restaurant listings, and navigate key app sections from a bottom navigation experience.

## Home Screen

<img width="1344" height="2992" alt="Screenshot_1775421972" src="https://github.com/user-attachments/assets/43b6c6db-c9c6-4608-843e-2038c99dead0"  />

## What is implemented

- App bootstrap with `Provider`-based state management in `lib/main.dart`
- Bottom navigation container in `lib/shared/app_bottom_nav.dart`
- Home flow in `lib/features/home/`:
  - Profile header (name, avatar, location)
  - Search input
  - Category row with SVG icons
  - Restaurant cards
- Explore and Favourite placeholder screens connected to view models

## Tech stack

- Flutter (Dart SDK `^3.10.4`)
- State management: `provider`
- UI/Icons: `google_fonts`, `iconly`, `flutter_svg`

## Project structure

```text
lib/
  core/                 # Theme, colors, and app-level configuration
  features/
    home/               # Home entities, repository, view model, widgets, screen
    explor/             # Explore screen + view model
    favourite/          # Favourite screen + view model
    profile/            # Profile feature (in progress)
  shared/               # Shared widgets and app navigation state
  main.dart             # App entry point
```

## Getting started

1. Install Flutter SDK and run `flutter doctor`
2. From this `mobile/` directory, install dependencies:

```bash
flutter pub get
```

3. Run the app:

```bash
flutter run
```

## Useful commands

```bash
flutter analyze
flutter test
```

## Notes

- Some tabs in the bottom navigation (`Shops`, `Orders`) are currently placeholders.
- Home data is currently mocked in `HomeRepositoryImpl` and `HomeViewModel` for UI development.
