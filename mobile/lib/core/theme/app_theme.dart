import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import 'app_colors.dart';

class AppTheme {
  static ThemeData lightTheme = ThemeData(
    useMaterial3: true,

    // COLORS
    colorScheme: ColorScheme.light(
      primary: AppColors.primary,
      secondary: AppColors.darkGreen,
      surface: AppColors.background,
    ),

    scaffoldBackgroundColor: AppColors.background,

    //  APP BAR
    appBarTheme: const AppBarTheme(
      backgroundColor: AppColors.background,
      foregroundColor: AppColors.darkGray,
      elevation: 0,
      centerTitle: false,
    ),

    // 🔤 TEXT
    textTheme: GoogleFonts.comfortaaTextTheme(
      const TextTheme(
        headlineLarge: TextStyle(
          fontSize: 24,
          fontWeight: FontWeight.bold,
          color: AppColors.darkGray,
        ),
        titleMedium: TextStyle(
          fontSize: 16,
          fontWeight: FontWeight.w600,
          color: AppColors.darkGray,
        ),
        bodyLarge: TextStyle(fontSize: 14, color: AppColors.darkGray),
        bodyMedium: TextStyle(fontSize: 12, color: AppColors.mediumGray),
      ),
    ),

    // 🔘 BUTTONS
    elevatedButtonTheme: ElevatedButtonThemeData(
      style: ElevatedButton.styleFrom(
        backgroundColor: AppColors.primary,
        foregroundColor: Colors.white,
        elevation: 0,
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
        padding: const EdgeInsets.symmetric(vertical: 14),
      ),
    ),

    // 🧾 INPUT FIELDS (search bars, forms)
    inputDecorationTheme: InputDecorationTheme(
      filled: true,
      fillColor: Colors.white,
      hintStyle: const TextStyle(color: AppColors.mediumGray),
      border: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: BorderSide.none,
      ),
    ),
  );
}
