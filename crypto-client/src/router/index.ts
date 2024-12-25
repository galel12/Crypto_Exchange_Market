import { createRouter, createWebHistory } from "vue-router";

import SignUp from "../views/SignUp.vue";
import Login from "../views/Login.vue";
import Wallet from "../views/Wallet.vue";

const routes = [
  { path: "/", component: Login },
  { path: "/signup", component: SignUp },
  {
    path: "/wallet",
    component: Wallet,
    beforeEnter: (_to, _from, next) => {
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