# Contributing to Replate

Welcome! Thank you for considering contributing to the Replate project. This document outlines the process and standards for contributing code, documentation, and ideas to ensure a smooth and collaborative workflow.

---

## 📋 Table of Contents
- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Code Standards](#code-standards)
- [Commit Guidelines](#commit-guidelines)
- [Pull Request Process](#pull-request-process)
- [Resolving Conflicts](#resolving-conflicts)
- [Asking for Help](#asking-for-help)

---

## 🚀 Getting Started

1. **Fork the repository** (if external) or create a new branch from `main` (if internal).
2. **Clone your fork/branch**:
   ```bash
   git clone <repository-url>
   cd replate/backend
   ```
3. **Install prerequisites**:
   - .NET 10 SDK
   - Rider or Visual Studio 2022+
   - SQL Server (LocalDB or full instance)
   - Git
4. **Restore dependencies**:
   ```bash
   dotnet restore
   ```
5. **Apply migrations and run the app**:
   ```bash
   dotnet ef database update --project Replate.Infrastructure --startup-project Replate.Api
   dotnet run --project Replate.Api
   ```
6. **Verify**: Open Swagger UI at `https://localhost:5001`.

---

## 🔄 Development Workflow

1. **Create a feature branch**:
   ```bash
   git checkout -b feature/<short-description>
   ```
2. **Make changes** in your branch. Follow the [Code Standards](#code-standards).
3. **Write or update tests** for your changes.
4. **Commit** with a clear message (see [Commit Guidelines](#commit-guidelines)).
5. **Pull latest changes** from `main` and resolve any conflicts:
   ```bash
   git fetch origin
   git rebase origin/main
   ```
6. **Push your branch**:
   ```bash
   git push origin feature/<short-description>
   ```
7. **Open a Pull Request (PR)** against `main`.
8. **Request review** from a team member.
9. **Address feedback** and update your PR as needed.
10. **Merge** after approval and all checks pass.

---

## 📝 Code Standards

- Follow the [DEVELOPMENT.md](docs/DEVELOPMENT.md) for architecture, naming, and file structure.
- Use Clean Architecture and CQRS patterns.
- Use dependency injection for all services and handlers.
- Write clear, self-documenting code and add comments where necessary.
- Keep methods short and focused.
- Write or update unit/integration tests for all new features and bug fixes.
- Do not commit secrets or sensitive data.

---

## 💬 Commit Guidelines

- Use clear, descriptive commit messages.
- Format: `<type>(<scope>): <description>`
- Types: `feat`, `fix`, `docs`, `refactor`, `test`, `chore`
- Example:
  ```
  feat(vendor): add vendor profile creation endpoint
  fix(deal): resolve null reference in deal handler
  docs(readme): update setup instructions
  ```

---

## 🔍 Pull Request Process

- PR title should follow commit message format.
- Provide a clear description of what your PR does and why.
- Link to any related issues.
- Ensure your branch is up to date with `main` before merging.
- All checks (build, tests) must pass before merging.
- At least one team member must review and approve the PR.

---

## ⚠️ Resolving Conflicts

- Always pull/rebase from `main` before pushing your branch.
- If you encounter conflicts:
  1. Use `git status` to see conflicting files.
  2. Open and resolve conflicts in your editor.
  3. Add resolved files: `git add <file>`
  4. Continue rebase/merge: `git rebase --continue` or `git merge --continue`
  5. Push your branch.
- If unsure, ask for help before force-pushing.

---

## 🆘 Asking for Help

- Check the `/docs` folder for architecture, API, and development guidelines.
- Ask questions in the team chat or open a GitHub Discussion.
- For urgent issues, tag a maintainer in your PR or issue.

---

Thank you for contributing to Replate! 🎉
