import { Package } from "./package";

export interface Cart {
    cartId: number;
    cartQuantity?: number;
    cartAmount: number;
    cartStatusId?: number;
    //packageId?: number;
    packages: Package[]; // One-to-Many relationship
}
