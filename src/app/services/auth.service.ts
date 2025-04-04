import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  signup(user: { email: string; password: string }) {
    localStorage.setItem(user.email, JSON.stringify(user));
  }

  login(email: string, password: string): boolean {
    const user = JSON.parse(localStorage.getItem(email) || '{}');
    return user.password === password;
  }
}
