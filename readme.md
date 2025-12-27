# 📁 FileStack – Secure File Storage Platform

FileStack is a **Google Drive–like application** that allows users to **upload, manage, and organize their personal files securely**.

The system is built using **Clean Architecture** and the **CQRS pattern (via MediatR)** to ensure scalability, maintainability, and clear separation of concerns.

---

## 🔐 Authentication & Email Verification

FileStack implements a **secure OTP-based email verification flow** before allowing users to access their storage space.

### Key Features

* User registration with email
* Temporary user storage until verification
* OTP-based email confirmation
* OTP expiration & attempt limits
* Secure password hashing
* Identity user creation after verification

---

## 🧱 Architecture

* **Clean Architecture** (Application, Infrastructure, API layers)
* **CQRS with MediatR** for command handling
* **ASP.NET Identity** for user management
* **EF Core** for persistence

Command handlers remain thin and delegate business logic to services.

---

## ✉️ OTP Verification Flow

1. User registers → stored as `TempUser`
2. OTP is generated, hashed, and emailed
3. User submits OTP
4. OTP is validated (email-bound, time-limited, attempt-limited)
5. Identity user is created with `EmailConfirmed = true`
6. TempUser is removed

✔ OTPs cannot be reused
✔ One OTP belongs to one email only

---

## 🔐 Security Highlights

* Passwords are never stored in plain text
* OTPs are hashed and invalidated after use
* Generic responses prevent user enumeration
* Transactions ensure atomic operations

---

## 📦 Core Functionality

Once authenticated, users can:

* Upload files
* Manage personal folders
* Access files securely

(Designed as a scalable foundation for a Google Drive–style system.)

---

## ⭐ Status

✔ Production-ready authentication & verification flow

🚧 File management features are actively evolving

---

Built with scalability, security, and clean design principles in mind 🚀
