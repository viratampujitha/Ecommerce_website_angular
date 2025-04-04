import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private products = [
    { id: 1, name: 'Tomato', price: 30, image: '/assets/images/tomato.jpg' },
    { id: 2, name: 'Potato', price: 25, image: '/assets/images/potato.jpg' },
    { id: 3, name: 'Carrot', price: 40, image: '/assets/images/carrot.jpg' }
  ];

  getProducts() {
    return this.products;
  }
}
