<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router';
</script>

<template>
  <div class="rounded drop-shadow-lg">
    <div class="max-w-5xl m-auto flex flex-col h-[95vh]">
      <div class="self-center"><img src="/banner.png" class="h-36"  alt="Canary Slushie Logo"/></div>
      <div class="bg-[#fe6d73] flex justify-center rounded-t">
        <nav class="w-1/2 justify-evenly flex p-2 font-bold">
          <RouterLink
            to="/"
            class="hover:bg-[#b64c4b] p-2 rounded w-20 bg-[#ffcb77] text-center text-[#5C4B51] hover:text-white"
          >Pets</RouterLink
          >
          <RouterLink
            to="items"
            class="hover:bg-[#b64c4b] p-2 rounded w-20 bg-[#ffcb77] text-center text-[#5C4B51] hover:text-white"
          >Items</RouterLink
          >
        </nav>
      </div>

      <main class="bg-gray-200 pt-4 grow">
        <RouterView />
      </main>

      <div class="rounded-b bg-[#fe6d73] flex flex-col items-center p-2 text-white">
        <p>This site is not affiliated with Chicken Smoothie</p>
        <div class="flex gap-2">
          <p><a href="https://www.inprnt.com/gallery/starlingspacey/">Logo Art by Spacey3D</a></p>
          <p>|</p>
          <p class="text-xs h-fit self-center">Special thanks to HAMHamReborn and Wietlol</p>
        </div>
      </div>
    </div>
  </div>
</template>