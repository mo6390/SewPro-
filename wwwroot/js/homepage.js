function showCards(sectionId) {
    // إخفاء جميع الأقسام إذا كانت موجودة
    document.querySelectorAll('.homepage-cards').forEach(function(card) {
        card.style.display = 'none';
    });

    const section = document.getElementById(sectionId + '-cards');
    if (section) {
        // إظهار القسم المحدد فقط إذا كان موجودًا
        section.style.display = 'flex';
    } else {
        console.error("القسم المحدد غير موجود: " + sectionId);
    }
}
