import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';
import { CartService } from '../../services/cart.service';

@Component({
  standalone: true,
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
  imports: [CommonModule]
})
export class ProductsComponent {
  productService = inject(ProductService);
  cartService = inject(CartService);

  products = this.productService.getProducts();  // ✅ Get products from service

  constructor() {}

  addToCart(product: { id: number; name: string; price: number; image: string }) {
    this.cartService.addToCart(product);
    alert(`${product.name} added to cart!`);
  }
}
