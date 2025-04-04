import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  imports: [CommonModule, FormsModule]
})
export class SignupComponent {
  email = '';
  password = '';
  confirmPassword = '';  // ✅ Added confirmPassword field

  constructor(private authService: AuthService, private router: Router) {}

  signup() {
    if (this.password !== this.confirmPassword) {
      alert("Passwords do not match!");  // ✅ Added validation
      return;
    }

    this.authService.signup({ email: this.email, password: this.password });
    alert("Signup successful! Redirecting to login...");
    this.router.navigate(['/login']);
  }
}
