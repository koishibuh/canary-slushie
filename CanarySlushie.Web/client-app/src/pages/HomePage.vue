<script setup lang="ts">
import { ref } from 'vue';
import petsJson from '@/data/pets.json';

const pets = petsJson;
const input = ref('');
const archivePageUrl = ref('');
const cleanBackground = ref('');
const displayResult = ref('invisible');
const eventTitle = ref('');
const petFound = ref(true);

function cleanImageUrl(petImageUrl: string): string {
  return petImageUrl
    .replace('https://static.chickensmoothie.com/', '')
    .replace('pic.php?k=', '')
    .replace('archive/image.php?k=', '')
    .replace(/&.*/g, "$'");
}

function createEventTitle(archivePageUrl: string) {
  eventTitle.value = archivePageUrl
    .replace(/^(?:[^/]*\/){4}/g, '')
    .replace(/(%20|\/)/g, ' ')
    .replace(/(%27|\/)/g, "'");
}

function adjustImageBackground(petImageUrl: string) {
  cleanBackground.value = petImageUrl.replace(/&.*/g, "$'") + '&bg=d1d5db';
}

function findArchiveUrl(petImageUrl: string) {
  /* cleanBackground.value = adjustBackgroundColor(petImageUrl); */
  const petImageId = cleanImageUrl(petImageUrl);

  let result = pets.find((group) =>
    group.petImageList.some((petId) => petId === petImageId)
  )?.archiveUrl;
  if (result) {
    adjustImageBackground(petImageUrl);
    console.log(result);
    createEventTitle(result);
    archivePageUrl.value = result;
    console.log(result);
    petFound.value = true;
    displayResult.value = 'visible';
  } else {
    petFound.value = false;
    archivePageUrl.value = '';
    eventTitle.value = '';
    cleanBackground.value = '';
  }

  input.value = '';
}
</script>

<template>
  <h1 class="text-center">Pet Search</h1>

  <div class="md:w-2/3 m-auto p-2">
    <p>A handy tool to find which archive page your CS pet is on!</p>
    <p>
      Begin your search by right clicking on your pet, copying it's image url, and pasting the link
      in the search field below. Unfortunately this will not work on pets with dress up items.
    </p>
  </div>

  <div class="bg-gray-300 p-4 md:w-2/3 m-auto rounded mb-2">
    <div class="flex flex-col mb-4">
      <form @submit.prevent="findArchiveUrl(input)">
        <label>Enter Pet Image Url:</label>
        <div class="flex gap-2">
          <input type="text" v-model="input" placeholder="" class="px-2 w-2/3" />
          <button class="primary-button w-1/3">Send</button>
        </div>
      </form>
    </div>

    <div class="flex flex-col items-center border rounded p-4">
      <img :src="cleanBackground" class="object-contain max-h-60" />
      <div v-if="!petFound" class="flex flex-col items-center p-4">
        <p>❗Pet Not Found❗</p>
        <p>Either the image url provided is incorrect or the pet has not recorded yet.</p>
      </div>
      <div :class="displayResult" class="flex flex-col gap-2" v-else>
        <p class="font-bold text-center">{{ eventTitle }}</p>
        <a :href="archivePageUrl" target="_blank" class="primary-button text-black text-center"
          >View Archive Page</a
        >
      </div>
    </div>
  </div>
</template>