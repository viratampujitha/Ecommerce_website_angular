import { Component, inject } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  imports: [CommonModule]
})
export class CartComponent {
  cartService = inject(CartService);  
  cart = this.cartService.getCartItems(); 

  constructor() {}

  increaseQuantity(product: any) {
    product.quantity++;
  }

  decreaseQuantity(product: any) {
    if (product.quantity > 1) {
      product.quantity--;
    } else {
      this.removeFromCart(product);
    }
  }

  removeFromCart(product: any) {
    this.cart = this.cart.filter(item => item.id !== product.id);
    this.cartService.updateCart(this.cart);
  }

  checkout() {
    alert('Proceeding to checkout!');
    this.cartService.clearCart();
    this.cart = [];
  }
}
