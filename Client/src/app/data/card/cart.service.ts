import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

interface CartItem {
  id: string | undefined;
  title: string | undefined;
  price: number;
  image: string | undefined;
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class CartService {
  cartItems: CartItem[] = []
  private cart = new BehaviorSubject<CartItem[]>([]);
  cart$ = this.cart.asObservable();

  AddToCard(item: CartItem): void{
    const existingItem = this.cartItems.find(cartItem => cartItem.id === item.id);
    if (existingItem) {
      existingItem.quantity += 1;
    } else {
      this.cartItems.push(item);
    }
    this.cart.next([...this.cartItems]);
    localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
  }

  removeFromCart(id: string): void {
    this.cartItems = this.cartItems.filter(item => item.id !== id);
    this.cart.next([...this.cartItems]);
    localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
  }

  DeleteOneQuantity(item: CartItem){
    if (item.quantity > 1) {
      const existingItem = this.cartItems.find(cartItem => cartItem.id === item.id);
      if (!existingItem) { return ;}
      item.quantity = existingItem.quantity -= 1;
      localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
    }
  }

  clearCart(): void {
    this.cartItems = [];
    this.cart.next([...this.cartItems]);
    localStorage.removeItem('cartItems');
  }

  getTotal(): number {
    return this.cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
  }

  getTotalBook(): number {
    return this.cartItems.length;
  }

  getBook() {
    return this.cartItems;
  }


  constructor() { 
    const savedItems = localStorage.getItem('cartItems');
    if (savedItems) {
        this.cartItems = JSON.parse(savedItems);
        this.cart.next(this.cartItems);
    }
  }
}
