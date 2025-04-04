import { Component } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-order-confirmation',
  templateUrl: './order-confirmation.component.html',
  styleUrls: ['./order-confirmation.component.css'],
  imports: [CommonModule]
})
export class OrderConfirmationComponent {
  orderPlaced = false;

  constructor(private cartService: CartService, private router: Router) {}

  confirmOrder() {
    this.cartService.clearCart();
    this.orderPlaced = true;
    setTimeout(() => this.router.navigate(['/products']), 3000);
  }
}
