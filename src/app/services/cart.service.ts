import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cart: any[] = [];

  constructor() {}

  getCartItems() {
    return this.cart;
  }

  addToCart(item: any) {
    const existingItem = this.cart.find(cartItem => cartItem.id === item.id);
    if (existingItem) {
      existingItem.quantity++;  // Increase quantity if item exists
    } else {
      this.cart.push({ ...item, quantity: 1 });
    }
  }

  removeFromCart(itemId: number) {
    const index = this.cart.findIndex(cartItem => cartItem.id === itemId);
    if (index !== -1) {
      if (this.cart[index].quantity > 1) {
        this.cart[index].quantity--;  // Reduce quantity
      } else {
        this.cart.splice(index, 1);  // Remove item if quantity is 1
      }
    }
  }

  updateCart(updatedCart: any[]) {
    this.cart = updatedCart;
  }

  clearCart() {
    this.cart = [];
  }
}
