import { createRouter, createWebHistory } from "vue-router";
import type { RouteLocationNormalized, NavigationGuardNext } from "vue-router";

import SignUp from "../views/Signup.vue";
import Login from "../views/Login.vue";
import Wallet from "../views/Wallet.vue";

const routes = [
  { path: "/", component: Login },
  { path: "/signup", component: SignUp },
  {
    path: "/wallet",
    component: Wallet,
    beforeEnter: (to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
      const token = localStorage.getItem("token");
      if (token) {
        next();
      } else {
        next("/");
      }
    },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;