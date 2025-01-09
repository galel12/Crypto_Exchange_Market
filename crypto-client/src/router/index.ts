import { createRouter, createWebHistory } from "vue-router";
import Login from "../components/Login.vue";
import SignUp from "../components/SignUp.vue";
import Wallet from "../components/Wallet.vue";

const routes = [
  { path: "/", name: "Login", component: Login },
  { path: "/signup", name: "SignUp", component: SignUp },
  { path: "/wallet", name: "Wallet", component: Wallet,
    beforeEnter: (_to: any, _from: any, _next: any) => {
      const token = localStorage.getItem("token");
      if (!token) {
        alert("Access denied. Please login first.");
        _next("/"); 
      } else {
        _next(); 
      }
    },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;